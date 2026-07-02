using System.Diagnostics;
using KariyerYonetimi.Data;
using KariyerYonetimi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public async Task<IActionResult> Hakkimizda()
        {
            SirketBilgisi sirket = new SirketBilgisi();

            sirket.Id = 1;
            sirket.SirketAdi = "Kariyer Yönetimi A.Þ.";
            sirket.Sektor = "Eðitim";
            sirket.KurulusYili = 2024;
            sirket.KacYillik = DateTime.Now.Year - sirket.KurulusYili;
            sirket.PersonelSayisi = await _context.Personeller.CountAsync();
            return View(sirket);
        }

        async public Task<IActionResult> Personeller()
        {
            return View(await _context.Personeller.Include(p => p.Departman).ToListAsync());
        }

        async public Task<IActionResult> PersonelDetay(int id)
        {
            var bulunanPersonel = await _context.Personeller.Include(p => p.Departman).FirstOrDefaultAsync(p => p.Id == id);
            return View(bulunanPersonel);
        }

        [HttpGet]
        async public Task<IActionResult> PersonelEkle()
        {
            ViewBag.Departmanlar = await _context.Departmanlar.Select(d => new SelectListItem { Text = d.Ad, Value = d.Id.ToString() }).ToListAsync();
            return View();
        }

        [HttpPost]
        async public Task<IActionResult> PersonelEkle(Personel p)
        {
            if (ModelState.IsValid)
            {
                _context.Personeller.Add(p);
                await _context.SaveChangesAsync();
                return RedirectToAction("Personeller");
            }
            ViewBag.Departmanlar = await _context.Departmanlar.Select(d => new SelectListItem { Text = d.Ad, Value = d.Id.ToString() }).ToListAsync();
            return View(p);
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
                ViewBag.Departmanlar = await _context.Departmanlar.Select(d => new SelectListItem { Text = d.Ad, Value = d.Id.ToString() }).ToListAsync();
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
                personelToUpdate.Telefon2 = guncellenenPersonel.Telefon2;
                personelToUpdate.DepartmanId = guncellenenPersonel.DepartmanId;
                personelToUpdate.Maas = guncellenenPersonel.Maas;

                await _context.SaveChangesAsync();
                return RedirectToAction("Personeller");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
