using SQLite;

namespace MHike.Model
{
    public class HikeModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        //public string Image { get; set; }
        public string Location { get; set; }
        public string Date { get; set; }
        public string Length { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Parking { get; set; }
        public string CreatedAt { get; set; }
        public string LastUpdated { get; set; }
    }
}
