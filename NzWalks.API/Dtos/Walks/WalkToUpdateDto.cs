namespace NzWalks.API.Dtos.Walks
{
    public class WalkToUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string ImageUrl { get; set; }
        public long DifficultyId { get; set; }
        public long RegionId { get; set; }
    }
}
