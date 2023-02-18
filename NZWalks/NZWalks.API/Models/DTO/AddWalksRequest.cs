namespace NZWalks.API.Models.DTO
{
    public class AddWalksRequest
    {
        public string Name { get; set; }

        public decimal Length { get; set; }
        public Guid RegionID { get; set; }
        public Guid WalkDifficultyID { get; set; }
    }
}
