using System.Collections.Generic;

namespace NilDevStudio.Domain
{
    public class Speaker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Curriculum { get; set; }
        public string ImageURL { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<EventSpeaker> EventSpeaker { get; set; }
    }
}