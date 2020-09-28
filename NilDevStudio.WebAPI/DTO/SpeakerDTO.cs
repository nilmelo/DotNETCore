using System.Collections.Generic;

namespace NilDevStudio.WebAPI.DTO
{
    public class SpeakerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Curriculum { get; set; }
        public string ImageURL { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<SocialNetworkDTO> SocialNetworks { get; set; }
        public List<MyEventDTO> MyEvents { get; set; }
    }
}
