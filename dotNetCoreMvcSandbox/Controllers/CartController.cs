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
        public async Task<IActionResult> Add([FromBody] long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var product = await _context.Product.SingleOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.SingleOrDefaultAsync(x => x.SessionId == HttpContext.Session.Id);

            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = HttpContext.Session.Id
                };

                await _context.AddAsync(cart);
            }

            var cartItem = AddToCart(cart, product);

            await _context.SaveChangesAsync();

            return View();
        }

        private CartItem GetCartItemInstance(Cart cart, Product product, double price, int qty = 1)
        {
            return new CartItem
            {
                Cart = cart,
                Product = product,
                Price = product.Price,
                Quantity = qty
            };
        }

        private CartItem AddToCart(Cart cart, Product product)
        {
            CartItem item = cart.CartItems.FirstOrDefault(x => x.Product == product);
            if (item == null)
            {
                item = GetCartItemInstance(cart, product, product.Price);
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