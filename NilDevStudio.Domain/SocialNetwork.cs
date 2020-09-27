namespace NilDevStudio.Domain
{
    public class SocialNetwork
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public int? EventId { get; set; }
        public MyEvent MyEvent { get; }
        public int? SpeakerId { get; set; }
        public Speaker Speaker { get; }
    }
}
