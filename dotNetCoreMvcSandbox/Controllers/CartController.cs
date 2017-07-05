using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dotNetCoreMvcSandbox.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using dotNetCoreMvcSandbox.Extensions;

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
        [Produces("application/json")]
        public async Task<JsonResult> Add([FromBody] long id)
        {
            if (id == 0)
            {
                return Json(false);
            }

            var product = await _context.Product.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return Json(false);
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(x => x.SessionId == HttpContext.Session.Id);
            if (cart == null)
            {
                cart = CreateCartInstance(HttpContext.Session.Id);
                await _context.AddAsync(cart);
            }

            var cartItem = AddToCart(cart, product);

            await _context.SaveChangesAsync();

            return Json(true);
        }

        private CartItem CreateCartItemInstance(Cart cart, Product product, double price, int qty = 1)
        {
            return new CartItem
            {
                Cart = cart,
                Product = product,
                Price = product.Price,
                Quantity = qty
            };
        }

        private Cart CreateCartInstance(string sessionId)
        {
            return new Cart
            {
                SessionId = sessionId
            };
        }

        private CartItem AddToCart(Cart cart, Product product)
        {
            CartItem item = cart.CartItems.FirstOrDefault(x => x.Product == product);
            if (item == null)
            {
                item = CreateCartItemInstance(cart, product, product.Price);
                cart.CartItems.Add(item);
            }
            else
            {
                item.Quantity++;
            }

            return item;
        }
    }
}