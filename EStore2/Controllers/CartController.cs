using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EStore2.Models;
using Microsoft.AspNetCore.Mvc;

namespace EStore2.Controllers
{
    public class CartController : Controller
    {
        public readonly MyeStoreContext db;

        public CartController(MyeStoreContext context)
        {
            db = context;
        }

        public List<CartItem> Carts {
            get {
                List<CartItem> myCart = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if(myCart == default(List<CartItem>))
                {
                    myCart = new List<CartItem>();
                }
                return myCart;
            }
        }

        public IActionResult DeleteItemCart(int mahh)
        {
            List<CartItem> giohang = Carts;
            CartItem item = giohang.SingleOrDefault(p => p.MaHh == mahh);
            if(item != null)
            {
                giohang.Remove(item);
                HttpContext.Session.Set("GioHang", giohang);
            }
            return RedirectToAction("Index");
        }
        
        public IActionResult AddtoCart(int mahh)
        {
            // lấy giỏ hàng đang có
            List<CartItem> giohang = Carts;
            // kiểu tra xem hàng đã có trong giỏ chưa
            CartItem item = giohang.SingleOrDefault(p => p.MaHh == mahh);

            if(item != null)
            {
                item.SoLuong++;
            }
            else {
                HangHoa hh = db.HangHoa.SingleOrDefault(p => p.MaHh == mahh);
                item = new CartItem
                {
                    MaHh = mahh,
                    SoLuong = 1,
                    TenHh = hh.TenHh,
                    Hinh = hh.Hinh,
                    GiaBan = hh.DonGia.Value * (1 - hh.GiamGia)
                };
                giohang.Add(item);
            }
            // lưu session
            HttpContext.Session.Set("GioHang", giohang);
            return RedirectToAction("Index");
        }

        public IActionResult UpdateGioHang(int mahh,int sl)
        {

            List<CartItem> giohang = Carts;
            // kiểu tra xem hàng đã có trong giỏ chưa
            CartItem item = giohang.SingleOrDefault(p => p.MaHh == mahh);
            if (item != null)
            {
                item.SoLuong = sl;
            }
            HttpContext.Session.Set("GioHang", giohang);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            return View(Carts);
        }
    }
}