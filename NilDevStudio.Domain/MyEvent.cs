using System.Collections.Generic;
using System;

namespace NilDevStudio.Domain
{
    public class MyEvent
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public DateTime DateEvent { get; set; }
        public string Theme { get; set; }
        public int QuantPeople { get; set; }
        public string ImageURL { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public List<Lot> Lots { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<EventSpeaker> EventSpeakers { get; set; }
    }
}
