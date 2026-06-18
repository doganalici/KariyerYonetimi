using System.Diagnostics;
using KariyerYonetimi.Data;
using KariyerYonetimi.Models;
using Microsoft.AspNetCore.Mvc;

namespace KariyerYonetimi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KariyerYonetimiDbContext _context;

        public HomeController(ILogger<HomeController> logger, KariyerYonetimiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Hakkimizda()
        {
            SirketBilgisi sirket = new SirketBilgisi();

            sirket.Id = 1;
            sirket.SirketAdi = "Kariyer Yönetimi A.Þ.";
            sirket.Sektor = "Eðitim";
            sirket.KurulusYili = 2024;
            sirket.KacYillik = DateTime.Now.Year - sirket.KurulusYili;
            sirket.PersonelSayisi = 50;


            ////VievBag.ÝstediðinÝsim="Deðer";
            //ViewBag.SirketAdi ="Kariyer Yönetimi A.Þ.";
            //ViewBag.KurulusYili = 2024;
            //ViewBag.KacYillik= DateTime.Now.Year - ViewBag.KurulusYili;
            return View(sirket);
        }

        public IActionResult Personeller()
        {
            //List<Personel> personelListesi = new List<Personel>()
            //{
            //    new Personel{Id=1,Ad="Ahmet",Soyad="Yýlmaz",Email="ahmetyilmaz@example.com",Telefon="1234567890",Unvan="Müdür",Maas=70000},
            //    new Personel{Id=2,Ad="Ayþe",Soyad="Demir",Email="aysedemir@example.com",Telefon="0987654321",Unvan="Uzman",Maas=40000},
            //    new Personel{Id=3,Ad="Mehmet",Soyad="Kara",Email="mehmetkara@example.com" ,Telefon="5555555555",Unvan="Stajyer",Maas=20000}
            //};
            return View(_context.Personeller.ToList());
        }

        public IActionResult PersonelDetay(int id)
        {
            //List<Personel> personelListesi = new List<Personel>()
            //{
            //     new Personel{Id=1,Ad="Ahmet",Soyad="Yýlmaz",Email="ahmetyilmaz@example.com",Telefon="1234567890",Unvan="Müdür",Maas=70000},
            //    new Personel{Id=2,Ad="Ayþe",Soyad="Demir",Email="aysedemir@example.com",Telefon="0987654321",Unvan="Uzman",Maas=40000},
            //    new Personel{Id=3,Ad="Mehmet",Soyad="Kara",Email="mehmetkara@example.com" ,Telefon="5555555555",Unvan="Stajyer",Maas=20000}
            //};
            var bulunanPersonel = _context.Personeller.FirstOrDefault(p => p.Id == id);
            return View(bulunanPersonel);
        }

        [HttpGet]
        public IActionResult PersonelEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PersonelEkle(Personel yeniPersonel)
        {
            _context.Personeller.Add(yeniPersonel);
            _context.SaveChanges();
            return RedirectToAction("Personeller");
        }

        public IActionResult PersonelSil(int id)
        {
            var personelToRemove = _context.Personeller.FirstOrDefault(p => p.Id == id);
            if (personelToRemove != null)
            {
                _context.Personeller.Remove(personelToRemove);
                _context.SaveChanges();
            }
            return RedirectToAction("Personeller");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
