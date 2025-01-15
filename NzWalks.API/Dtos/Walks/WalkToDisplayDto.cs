namespace NzWalks.API.Dtos.Walks
{
    public class WalkToDisplayDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string ImageUrl { get; set; }
    }
}
