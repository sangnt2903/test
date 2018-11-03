using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EStore2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EStore2.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly MyeStoreContext db;

        public HangHoaController(MyeStoreContext _db)
        {
            db = _db;
        }
        public IActionResult Index(int? id)
        {
            List<HangHoa> hangHoas = new List<HangHoa>();
            // lấy danh sách hàng hóa theo Loại
            if (id.HasValue)
            {
                hangHoas = db.HangHoa.Where(p => p.MaLoai == id).ToList();
            }
            else
                hangHoas = db.HangHoa.ToList();
            return View(hangHoas);
        }

        public IActionResult ProductDetail(int? id)
        {
            HangHoa hh = new HangHoa();
            if (id.HasValue)
            {
                hh = db.HangHoa.Where(p => p.MaHh == id).SingleOrDefault();
            }
            else
            {
                return RedirectToAction("Index", "HangHoa");
            }
            return View(hh);
        }
    }
}