using System.ComponentModel.DataAnnotations;

namespace NilDevStudio.WebAPI.DTO
{
    public class LotDTO
    {
        public int Id { get; set; }

		[Required]
        public string Name { get; set; }

		[Required]
        public decimal Price { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }

		[Range(1, 2000)]
        public int Quantity { get; set; }
    }
}
