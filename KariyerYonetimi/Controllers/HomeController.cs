using System.Diagnostics;
using KariyerYonetimi.Data;
using KariyerYonetimi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        async public Task<IActionResult> Personeller()
        {
            //List<Personel> personelListesi = new List<Personel>()
            //{
            //    new Personel{Id=1,Ad="Ahmet",Soyad="Yýlmaz",Email="ahmetyilmaz@example.com",Telefon="1234567890",Unvan="Müdür",Maas=70000},
            //    new Personel{Id=2,Ad="Ayþe",Soyad="Demir",Email="aysedemir@example.com",Telefon="0987654321",Unvan="Uzman",Maas=40000},
            //    new Personel{Id=3,Ad="Mehmet",Soyad="Kara",Email="mehmetkara@example.com" ,Telefon="5555555555",Unvan="Stajyer",Maas=20000}
            //};
            return View(await _context.Personeller.ToListAsync());
        }

        async public Task<IActionResult> PersonelDetay(int id)
        {
            //List<Personel> personelListesi = new List<Personel>()
            //{
            //     new Personel{Id=1,Ad="Ahmet",Soyad="Yýlmaz",Email="ahmetyilmaz@example.com",Telefon="1234567890",Unvan="Müdür",Maas=70000},
            //    new Personel{Id=2,Ad="Ayþe",Soyad="Demir",Email="aysedemir@example.com",Telefon="0987654321",Unvan="Uzman",Maas=40000},
            //    new Personel{Id=3,Ad="Mehmet",Soyad="Kara",Email="mehmetkara@example.com" ,Telefon="5555555555",Unvan="Stajyer",Maas=20000}
            //};
            var bulunanPersonel = await _context.Personeller.FirstOrDefaultAsync(p => p.Id == id);
            return View(bulunanPersonel);
        }

        [HttpGet]
        async public Task<IActionResult> PersonelEkle()
        {
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> PersonelEkle(Personel yeniPersonel)
        {
            _context.Personeller.Add(yeniPersonel);
            await _context.SaveChangesAsync();
            return RedirectToAction("Personeller");
        }

        public async Task<IActionResult> PersonelSil(int id)
        {
            var personelToRemove = await _context.Personeller.FirstOrDefaultAsync(p => p.Id == id);
            if (personelToRemove != null)
            {
                _context.Personeller.Remove(personelToRemove);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Personeller");
        }

        [HttpGet]
        public async Task<IActionResult> PersonelGuncelle(int id)
        {
            var personelToUpdate = await _context.Personeller.FirstOrDefaultAsync(p => p.Id == id);
            if (personelToUpdate != null)
            {
                return View(personelToUpdate);
            }
            return RedirectToAction("Personeller");
        }

        [HttpPost]
        async public Task<IActionResult> PersonelGuncelle(Personel guncellenenPersonel)
        {
            var personelToUpdate = await _context.Personeller.FirstOrDefaultAsync(p => p.Id == guncellenenPersonel.Id);
            if (personelToUpdate != null)
            {
                personelToUpdate.Ad = guncellenenPersonel.Ad;
                personelToUpdate.Soyad = guncellenenPersonel.Soyad;
                personelToUpdate.Email = guncellenenPersonel.Email;
                personelToUpdate.Telefon = guncellenenPersonel.Telefon;
                personelToUpdate.Unvan = guncellenenPersonel.Unvan;
                personelToUpdate.Maas = guncellenenPersonel.Maas;

                await _context.SaveChangesAsync();
                return RedirectToAction("Personeller");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
