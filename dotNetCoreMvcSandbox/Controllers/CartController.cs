using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNetCoreMvcSandbox.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNetCoreMvcSandbox.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(long id)
        {
            //if (id == 0)
            //{
            //    return NotFound();
            //}

            //var cart = await _context.Cart.SingleOrDefaultAsync(x => x.SessionId == "1");
            //if (cart == null)
            //{
            //    var newCart = new Cart
            //    {
            //        SessionId = "1"
            //    };

            //    await _context.Cart.AddAsync(newCart);
            //}

            //CartItem currentItem = cart.CartItems.FirstOrDefault(x => x.ProductId == id);

            //if (currentItem == null)
            //{
            //    var newItem = new CartItem
            //    {
            //        Cart = cart,
                    
            //    }
            //    cart.CartItems.Add()
            //}
            //else
            //{
            //    currentItem.Quantity++;
            //}

            //await _context.SaveChangesAsync();

            //return View();
        }
    }
}