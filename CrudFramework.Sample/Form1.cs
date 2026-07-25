using CrudFramework.Core.Entities;
using CrudFramework.WinForms;
using System;
using System.Threading.Tasks;

namespace CrudFramework.Sample
{
    public partial class Form1 : CrudFormBase
    {
        public Form1()
        {
            InitializeComponent();
            EntityType = typeof(Customer);
        }
    }
}
