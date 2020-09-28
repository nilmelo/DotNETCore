using System.Collections.Generic;

namespace NilDevStudio.WebAPI.DTO
{
    public class MyEventDTO
    {
		public int Id { get; set; }
        public string Local { get; set; }
        public string DateEvent { get; set; }
        public string Theme { get; set; }
        public int QuantPeople { get; set; }
        public string ImageURL { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<LotDTO> Lots { get; set; }
        public List<SocialNetworkDTO> SocialNetworks { get; set; }
        public List<SpeakerDTO> Speakers { get; set; }
    }
}
