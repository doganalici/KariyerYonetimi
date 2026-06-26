namespace KariyerYonetimi.Models
{
    public class Departman
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public ICollection<Personel> Personeller { get; set; }
    }
}
