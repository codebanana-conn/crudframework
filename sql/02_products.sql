-- =====================================================================
--  CrudFramework — SQL fixture cho module demo "products" + "categories"
--  Chạy trên PostgreSQL. Contract giống customers:
--    fn_products_get(p_id int)          RETURNS jsonb -> 1 record | null
--    fn_products_list(p_filter jsonb)   RETURNS jsonb -> array các record
--    fn_products_upsert(p_payload jsonb) RETURNS jsonb -> {success,data,errors}
--    fn_products_delete(p_id int)       RETURNS jsonb -> {success,message}
--    fn_categories_get(p_id int)        RETURNS jsonb -> 1 record | null
--    fn_categories_list(p_filter jsonb) RETURNS jsonb -> array các record
-- =====================================================================

-- ---------- bảng categories ----------
CREATE TABLE IF NOT EXISTS categories (
    id            SERIAL PRIMARY KEY,
    category_name VARCHAR(100) NOT NULL UNIQUE
);

-- ---------- bảng products ----------
CREATE TABLE IF NOT EXISTS products (
    id               SERIAL PRIMARY KEY,
    product_code     VARCHAR(20)  NOT NULL UNIQUE,
    product_name     VARCHAR(200) NOT NULL,
    price            NUMERIC(18,2) NOT NULL DEFAULT 0,
    stock_quantity   INT          NOT NULL DEFAULT 0,
    is_available     BOOLEAN      NOT NULL DEFAULT TRUE,
    manufactured_date DATE        NULL,
    description      TEXT         NULL,
    category_id      INT          NULL REFERENCES categories(id),
    internal_note    TEXT         NULL,
    created_at       TIMESTAMPTZ  NOT NULL DEFAULT now()
);

-- =====================================================================
--  fn_categories_get
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_categories_get(p_id INT)
RETURNS jsonb
LANGUAGE sql
AS $$ SELECT to_jsonb(c) FROM (SELECT id, category_name FROM categories WHERE id = p_id) c; $$;

-- =====================================================================
--  fn_categories_list
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_categories_list(p_filter jsonb)
RETURNS jsonb
LANGUAGE sql
AS $$ SELECT COALESCE(jsonb_agg(to_jsonb(t) ORDER BY t.id), '[]'::jsonb)
       FROM (SELECT id, category_name FROM categories) t; $$;

-- =====================================================================
--  fn_products_get
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_products_get(p_id INT)
RETURNS jsonb
LANGUAGE sql
AS $$ SELECT to_jsonb(p) FROM (
    SELECT id, product_code, product_name, price, stock_quantity,
           is_available, manufactured_date, description, category_id, created_at
    FROM products WHERE id = p_id
) p; $$;

-- =====================================================================
--  fn_products_list — nhận filter object, trả JSON array
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_products_list(p_filter jsonb)
RETURNS jsonb
LANGUAGE plpgsql
AS $$ DECLARE
    v_keyword TEXT := NULLIF(p_filter->>'keyword','');
    v_avail   BOOLEAN := CASE WHEN p_filter ? 'is_available'
                               THEN (p_filter->>'is_available')::boolean ELSE NULL END;
    v_cat     INT := CASE WHEN p_filter ? 'category_id'
                          THEN NULLIF(p_filter->>'category_id','')::int ELSE NULL END;
    v_result  jsonb;
BEGIN
    SELECT COALESCE(jsonb_agg(to_jsonb(t) ORDER BY t.id), '[]'::jsonb)
    INTO v_result FROM (
        SELECT id, product_code, product_name, price, stock_quantity,
               is_available, manufactured_date, description, category_id, created_at
        FROM products
        WHERE (v_keyword IS NULL OR product_name ILIKE '%'||v_keyword||'%'
                                   OR product_code ILIKE '%'||v_keyword||'%')
          AND (v_avail   IS NULL OR is_available = v_avail)
          AND (v_cat     IS NULL OR category_id = v_cat)
    ) t;
    RETURN v_result;
END; $$;

-- =====================================================================
--  fn_products_upsert — validate + insert/update
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_products_upsert(p_payload jsonb)
RETURNS jsonb
LANGUAGE plpgsql
AS $$ DECLARE
    v_id      INT     := NULLIF(p_payload->>'id','')::int;
    v_code    TEXT    := NULLIF(p_payload->>'product_code','');
    v_name    TEXT    := NULLIF(p_payload->>'product_name','');
    v_price   NUMERIC := COALESCE(NULLIF(p_payload->>'price','')::numeric, 0);
    v_stock   INT     := COALESCE(NULLIF(p_payload->>'stock_quantity','')::int, 0);
    v_avail   BOOLEAN := COALESCE((p_payload->>'is_available')::boolean, TRUE);
    v_mfg     DATE    := NULLIF(p_payload->>'manufactured_date','')::date;
    v_desc    TEXT    := NULLIF(p_payload->>'description','');
    v_cat     INT     := NULLIF(p_payload->>'category_id','')::int;
    v_errors  jsonb   := '[]'::jsonb;
    v_row     products%ROWTYPE;
BEGIN
    -- validate
    IF v_code IS NULL THEN
        v_errors := v_errors || jsonb_build_object('field','product_code','message','Mã sản phẩm là bắt buộc.');
    END IF;
    IF v_name IS NULL THEN
        v_errors := v_errors || jsonb_build_object('field','product_name','message','Tên sản phẩm là bắt buộc.');
    END IF;
    IF v_price < 0 THEN
        v_errors := v_errors || jsonb_build_object('field','price','message','Giá không được âm.');
    END IF;
    -- trùng mã
    IF v_code IS NOT NULL AND EXISTS (
        SELECT 1 FROM products WHERE product_code = v_code AND (v_id IS NULL OR id <> v_id)
    ) THEN
        v_errors := v_errors || jsonb_build_object('field','product_code','message','Mã sản phẩm đã tồn tại.');
    END IF;

    IF jsonb_array_length(v_errors) > 0 THEN
        RETURN jsonb_build_object('success', false, 'data', NULL, 'errors', v_errors);
    END IF;

    IF v_id IS NULL THEN
        INSERT INTO products (product_code, product_name, price, stock_quantity,
            is_available, manufactured_date, description, category_id)
        VALUES (v_code, v_name, v_price, v_stock, v_avail, v_mfg, v_desc, v_cat)
        RETURNING * INTO v_row;
    ELSE
        UPDATE products SET
            product_code = v_code, product_name = v_name, price = v_price,
            stock_quantity = v_stock, is_available = v_avail,
            manufactured_date = v_mfg, description = v_desc, category_id = v_cat
        WHERE id = v_id RETURNING * INTO v_row;
        IF NOT FOUND THEN
            RETURN jsonb_build_object('success', false, 'data', NULL,
                'errors', jsonb_build_array(jsonb_build_object('field','id','message','Không tìm thấy bản ghi.')));
        END IF;
    END IF;

    RETURN jsonb_build_object('success', true,
        'data', jsonb_build_object('id',v_row.id,'product_code',v_row.product_code,
            'product_name',v_row.product_name,'price',v_row.price,
            'stock_quantity',v_row.stock_quantity,'is_available',v_row.is_available,
            'manufactured_date',v_row.manufactured_date,'description',v_row.description,
            'category_id',v_row.category_id,'created_at',v_row.created_at),
        'errors', '[]'::jsonb);
END; $$;

-- =====================================================================
--  fn_products_delete
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_products_delete(p_id INT)
RETURNS jsonb
LANGUAGE plpgsql
AS $$ BEGIN
    DELETE FROM products WHERE id = p_id;
    IF FOUND THEN RETURN jsonb_build_object('success', true, 'message', 'Đã xóa sản phẩm.');
    ELSE RETURN jsonb_build_object('success', false, 'message', 'Không tìm thấy sản phẩm để xóa.');
    END IF;
END; $$;

-- ---------- dữ liệu mẫu ----------
INSERT INTO categories (category_name) VALUES ('Điện tử'), ('Thời trang'), ('Thực phẩm')
ON CONFLICT (category_name) DO NOTHING;

INSERT INTO products (product_code, product_name, price, stock_quantity, is_available, manufactured_date, description, category_id)
VALUES ('SP001', N'Tivi LED 42"', 12900000, 15, TRUE, '2025-06-01', N'Tivi Samsung 42 inch Full HD', 1),
       ('SP002', N'Áo sơ mi nam', 350000, 200, TRUE, NULL, N'Sơ mi cotton trắng', 2),
       ('SP003', N'Cà phê Arabica', 250000, 0, FALSE, '2025-03-15', N'Cà phê rang mộc 250g', 3),
       ('SP004', N'Laptop Dell 5520', 15990000, 8, TRUE, '2025-09-01', N'Laptop i5-12450H, RAM 8GB', 1)
ON CONFLICT (product_code) DO NOTHING;
