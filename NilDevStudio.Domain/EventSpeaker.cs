namespace NilDevStudio.Domain
{
    public class EventSpeaker
    {
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
        public int EventId { get; set; }
        public MyEvent MyEvent { get; set; }
    }
}
