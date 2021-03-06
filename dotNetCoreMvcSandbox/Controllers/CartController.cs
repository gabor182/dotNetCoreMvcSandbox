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
            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {

            }

            return View(cart);
        }

        [Produces("application/json")]
        public JsonResult Info()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart");
            int productCount = 0;
            double productPriceSum = 0.0;

            if (cart != null)
            {
                productCount = cart.CartItems.Count * cart.CartItems.Sum(x => x.Quantity);
                productPriceSum = cart.CartItems.Sum(x => x.Price * x.Quantity);
            }

            return Json(new
            {
                count = productCount,
                sum = productPriceSum
            });
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<JsonResult> Add([FromBody] long id)
        {
            if (id == 0)
            {
                return Json(new { success = false, message = "Invalid ProductId." });
            }

            var product = await _context.Product.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Could not find product in database." });
            }

            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = CreateCartInstance(HttpContext.Session.Id);
                await _context.AddAsync(cart);
            }

            var cartItem = AddToCart(cart, product);

            await _context.SaveChangesAsync();
            HttpContext.Session.Set("Cart", cart);

            return Json(new
            {
                success = true,
                count = cart.CartItems.Count * cart.CartItems.Sum(x => x.Quantity),
                sum = cart.CartItems.Sum(x => x.Price * x.Quantity)
            });
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
            CartItem item = cart.CartItems.FirstOrDefault(x => x.ProductId == product.Id);
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