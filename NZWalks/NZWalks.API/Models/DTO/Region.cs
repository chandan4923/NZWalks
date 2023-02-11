using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class Region
    {
        public Guid ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public string Area { get; set; }

        public decimal Lat { get; set; }
        public decimal Long { get; set; }

        public long Population{ get; set; }


        //Navigation Property

        public IEnumerable<Walks> Walks { get; set; }
    }
}
