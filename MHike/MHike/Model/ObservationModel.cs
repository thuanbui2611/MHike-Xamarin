using SQLite;

namespace MHike.Model
{
    public class ObservationModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int HikeID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public string Time { get; set; }
        public string Comment { get; set; }
        //public string Image { get; set; }
        public string CreatedAt { get; set; }
        public string LastUpdated { get; set; }
    }
}
