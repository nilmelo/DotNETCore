using System.Collections.Generic;
using System;

namespace NilDevStudio.Domain
{
    public class MyEvent
    {
        public int Id { get; private set; }
        public string Local { get; private set; }
        public DateTime? DateEvent { get; private set; }
        public string Theme { get; private set; }
        public int QuantPeople { get; private set; }
        public string ImageURL { get; private set; }
        public string Telephone { get; private set; }
        public string Email { get; private set; }
        public List<Lot> Lots { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<EventSpeaker> EventSpeaker { get; private set; }
    }
}
