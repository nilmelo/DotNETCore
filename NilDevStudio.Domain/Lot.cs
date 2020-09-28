using System;

namespace NilDevStudio.Domain
{
    public class Lot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public int Quantity { get; set; }
        public int MyEventId { get; set; }
        public MyEvent MyEvent{ get; }
    }
}
