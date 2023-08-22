namespace cargosiklink.Models
{
    public class NumberTrack
    {
        public Guid Id { get; set; } 
        public string NumberTrackCode { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public string Link { get; set; }
        public double Weight { get; set; }
        public double Volume { get; set; }
        public string? Comment { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }


        public Guid StateId { get; set; }
        public State State { get; set; }

        public NumberTrack(string numberTrackCode, string description, 
                           Guid stateId, Guid userId,
                           double weight, double volume
                           , string link)
        {
            Id = Guid.NewGuid();
            NumberTrackCode = numberTrackCode;
            Description = description;
            StateId = stateId;
            UserId = userId;
            Weight = weight;
            Volume = volume;
            Link = link;
         }
    }
}
