using System;
using BPM.Persistency;

namespace BPM.Production
{
    public class Supply : IPersistent
    {
        public Guid Oid { get; set; }
        public Guid? UpdateUserId { get; set; }
        public DateTime UpdateTime { get; set; }

        public Supplier Supplier { get; set; }
        public Product Product { get; set; }
        public string Code { get; set; }
        public string SupplierCode { get; set; }

        public decimal Price { get; set; }
        public decimal Tax { get; set; }

        public string PriceScript { get; set; }
        

        public void AfterConstruction()
        {
        }


        public decimal CalculatePrice()
        {
            if (string.IsNullOrWhiteSpace(PriceScript))
                return Price;

            
        }
    }
}