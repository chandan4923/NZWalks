namespace NZWalks.API.Models.DTO
{
    public class Walks
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public decimal Length { get; set; }

        public Guid RegionID { get; set; }
        public Guid WalkDifficultyID { get; set; }

        //Navigation peroperty

        public API.Models.DTO.Region Region { get; set; }
        public API.Models.DTO.WalkDifficulty WalkDifficulties { get; set; }
    }
}
