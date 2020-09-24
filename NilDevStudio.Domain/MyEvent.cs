using System.ComponentModel.DataAnnotations;

namespace NilDevStudio.Domain
{
    public class MyEvent
    {
        [Key]
        public int EventId { get; set; }
        public string Local { get; set; }
        public string DataEvent { get; set; }
        public string theme { get; set; }
        public int QuantPeople { get; set; }
        public string Lot { get; set; }
    }
}