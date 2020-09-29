using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NilDevStudio.WebAPI.DTO
{
    public class MyEventDTO
    {
		public int Id { get; set; }

		[Required]
		[StringLength(100, MinimumLength = 4)]
        public string Local { get; set; }
        public string DateEvent { get; set; }

		[Required]
        public string Theme { get; set; }

		[Range(1, 2000)]
        public int QuantPeople { get; set; }
        public string ImageURL { get; set; }

		[Phone]
        public string Telephone { get; set; }

		[EmailAddress]
        public string Email { get; set; }
        public List<LotDTO> Lots { get; set; }
        public List<SocialNetworkDTO> SocialNetworks { get; set; }
        public List<SpeakerDTO> Speakers { get; set; }
    }
}
