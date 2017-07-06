using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMvcSandbox.Models
{
    public class CartItem
    {
        public long Id { get; set; }
        public long CartId { get; set; }
        public long ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public Cart Cart { get; set; }

        public Product Product { get; set; }
    }
}
