namespace NEB.Models
{
    public class Product
    {
        public Product(int? ID)
        {
            this.ID = ID;
        }
        public int? ID { get; }
        public string Title { get; set; }
        public decimal MinPrice { get; set; }

        public override string ToString()
        {
            return $"{Title}: {MinPrice}";
        }
    }
}