using System;

namespace NEB.Models
{
    public class Sale
    {
        public Sale(int? ID)
        {
            this.ID = ID;
        }
        public int? ID { get; }

        public decimal SumMoney { get; set; }
        public DateTime SoldTime { get; set; }
        public Manager Manager { get; set; }
        public Product Product { get; set; }

        public override string ToString()
        {
            return $"{Manager}, {Product} -> {SoldTime} {SumMoney}";
        }
    }
}