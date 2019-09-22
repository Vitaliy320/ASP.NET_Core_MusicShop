namespace WebApplication3.Models
{
    public class Purchase
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int InstrumentID { get; set; }

        public Customer Customer { get; set; }
        public Instrument Instrument { get; set; }
    }
}