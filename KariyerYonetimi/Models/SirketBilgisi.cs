namespace KariyerYonetimi.Models
{
    public class SirketBilgisi
    {
        public int Id { get; set; }
        public string SirketAdi { get; set; }
        public string Sektor { get; set; }
        public int KurulusYili { get; set; }
        public int KacYillik { get; set; }
        public int PersonelSayisi { get; set; }
    }
}
