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

            var (cart, isCartNew) = await GetCartBySessionIdAsync("1");

            var cartItem = AddToCart(cart, product);

            if (isCartNew)
            {
                await _context.AddAsync(cart);
            }

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
                Quantity = 1
            };
        }

        private async Task<(Cart cart, bool isNew)> GetCartBySessionIdAsync(string sessionId)
        {
            bool isNewInstance = false;
            var cart = await _context.Cart.SingleOrDefaultAsync(x => x.SessionId == sessionId);

            if (cart == null)
            {
                cart = new Cart
                {
                    SessionId = sessionId
                };
                isNewInstance = true;
            }

            return (cart: cart, isNew: isNewInstance);
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