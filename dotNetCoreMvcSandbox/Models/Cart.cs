using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMvcSandbox.Models
{
    public class Cart
    {
        public long Id { get; set; }
        public string SessionId { get; set; }
        public List<Product> Products { get; set; }
    }
}
