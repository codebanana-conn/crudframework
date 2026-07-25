-- =====================================================================
--  CrudFramework — SQL fixture cho module demo "customers"
--  Chạy trên PostgreSQL. Đây là "API" thật sự: C# chỉ gọi 4 function này.
--  Contract:
--    fn_customers_get(p_id int)         RETURNS jsonb  -> 1 record (object) | null
--    fn_customers_list(p_filter jsonb)  RETURNS jsonb  -> array các record
--    fn_customers_upsert(p_payload jsonb) RETURNS jsonb-> {success,data,errors}
--    fn_customers_delete(p_id int)      RETURNS jsonb  -> {success,message}
-- =====================================================================

-- ---------- bảng ----------
CREATE TABLE IF NOT EXISTS customers (
    id           SERIAL PRIMARY KEY,
    customer_code VARCHAR(20)  NOT NULL UNIQUE,
    customer_name VARCHAR(200) NOT NULL,
    birth_date    DATE         NULL,
    balance       NUMERIC(18,2) NOT NULL DEFAULT 0,
    is_active     BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at    TIMESTAMPTZ  NOT NULL DEFAULT now(),
    updated_at    TIMESTAMPTZ  NOT NULL DEFAULT now()
);

-- =====================================================================
--  fn_customers_get
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_customers_get(p_id INT)
RETURNS jsonb
LANGUAGE sql
AS $$
    SELECT to_jsonb(c) FROM (
        SELECT id, customer_code, customer_name, birth_date, balance, is_active
        FROM customers WHERE id = p_id
    ) c;
$$;

-- =====================================================================
--  fn_customers_list  — nhận filter object, trả JSON array
--  filter hỗ trợ: {"keyword": "...", "is_active": true, "from_date": "...", "to_date": "..."}
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_customers_list(p_filter jsonb)
RETURNS jsonb
LANGUAGE plpgsql
AS $$
DECLARE
    v_keyword   TEXT    := NULLIF(p_filter->>'keyword', '');
    v_is_active BOOLEAN := CASE WHEN p_filter ? 'is_active'
                                THEN (p_filter->>'is_active')::boolean ELSE NULL END;
    v_from      DATE    := NULLIF(p_filter->>'from_date','')::date;
    v_to        DATE    := NULLIF(p_filter->>'to_date','')::date;
    v_result    jsonb;
BEGIN
    SELECT COALESCE(jsonb_agg(to_jsonb(t) ORDER BY t.id), '[]'::jsonb)
    INTO v_result
    FROM (
        SELECT id, customer_code, customer_name, birth_date, balance, is_active
        FROM customers
        WHERE (v_keyword   IS NULL OR customer_name ILIKE '%'||v_keyword||'%'
                                   OR customer_code ILIKE '%'||v_keyword||'%')
          AND (v_is_active IS NULL OR is_active = v_is_active)
          AND (v_from      IS NULL OR birth_date >= v_from)
          AND (v_to        IS NULL OR birth_date <= v_to)
    ) t;

    RETURN v_result;
END;
$$;

-- =====================================================================
--  fn_customers_upsert — nhận payload, insert/update, validate
--  payload: {"id": null|int, "customer_code": "...", "customer_name": "...",
--            "birth_date": "yyyy-MM-dd"|null, "balance": number, "is_active": bool}
--  return : {"success": bool, "data": {...}, "errors": [{"field","message"}]}
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_customers_upsert(p_payload jsonb)
RETURNS jsonb
LANGUAGE plpgsql
AS $$
DECLARE
    v_id        INT     := NULLIF(p_payload->>'id','')::int;
    v_code      TEXT    := NULLIF(p_payload->>'customer_code','');
    v_name      TEXT    := NULLIF(p_payload->>'customer_name','');
    v_birth     DATE    := NULLIF(p_payload->>'birth_date','')::date;
    v_balance   NUMERIC := COALESCE(NULLIF(p_payload->>'balance','')::numeric, 0);
    v_active    BOOLEAN := COALESCE((p_payload->>'is_active')::boolean, TRUE);
    v_errors    jsonb   := '[]'::jsonb;
    v_row       customers%ROWTYPE;
BEGIN
    -- ----- validate -----
    IF v_code IS NULL THEN
        v_errors := v_errors || jsonb_build_object('field','customer_code','message','Mã khách hàng là bắt buộc.');
    END IF;
    IF v_name IS NULL THEN
        v_errors := v_errors || jsonb_build_object('field','customer_name','message','Tên khách hàng là bắt buộc.');
    END IF;
    IF v_balance < 0 THEN
        v_errors := v_errors || jsonb_build_object('field','balance','message','Số dư không được âm.');
    END IF;
    -- trùng mã (khác id đang sửa)
    IF v_code IS NOT NULL AND EXISTS (
        SELECT 1 FROM customers WHERE customer_code = v_code AND (v_id IS NULL OR id <> v_id)
    ) THEN
        v_errors := v_errors || jsonb_build_object('field','customer_code','message','Mã khách hàng đã tồn tại.');
    END IF;

    IF jsonb_array_length(v_errors) > 0 THEN
        RETURN jsonb_build_object('success', false, 'data', NULL, 'errors', v_errors);
    END IF;

    -- ----- insert / update -----
    IF v_id IS NULL THEN
        INSERT INTO customers (customer_code, customer_name, birth_date, balance, is_active)
        VALUES (v_code, v_name, v_birth, v_balance, v_active)
        RETURNING * INTO v_row;
    ELSE
        UPDATE customers
        SET customer_code = v_code,
            customer_name = v_name,
            birth_date    = v_birth,
            balance       = v_balance,
            is_active     = v_active,
            updated_at    = now()
        WHERE id = v_id
        RETURNING * INTO v_row;

        IF NOT FOUND THEN
            RETURN jsonb_build_object('success', false, 'data', NULL,
                'errors', jsonb_build_array(jsonb_build_object('field','id','message','Không tìm thấy bản ghi để cập nhật.')));
        END IF;
    END IF;

    RETURN jsonb_build_object(
        'success', true,
        'data', jsonb_build_object(
            'id', v_row.id, 'customer_code', v_row.customer_code,
            'customer_name', v_row.customer_name, 'birth_date', v_row.birth_date,
            'balance', v_row.balance, 'is_active', v_row.is_active),
        'errors', '[]'::jsonb);
END;
$$;

-- =====================================================================
--  fn_customers_delete
-- =====================================================================
CREATE OR REPLACE FUNCTION fn_customers_delete(p_id INT)
RETURNS jsonb
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM customers WHERE id = p_id;
    IF FOUND THEN
        RETURN jsonb_build_object('success', true, 'message', 'Đã xóa khách hàng.');
    ELSE
        RETURN jsonb_build_object('success', false, 'message', 'Không tìm thấy khách hàng để xóa.');
    END IF;
END;
$$;

-- ---------- dữ liệu mẫu ----------
INSERT INTO customers (customer_code, customer_name, birth_date, balance, is_active)
VALUES ('KH001', N'Nguyễn Văn A', '1990-05-20', 1500000, TRUE),
       ('KH002', N'Trần Thị B',   '1985-01-02',  250000, TRUE),
       ('KH003', N'Lê Văn C',     NULL,                0, FALSE)
ON CONFLICT (customer_code) DO NOTHING;
