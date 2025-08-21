using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager
{
    public record Order( string Product, int Quantity, string Name, string Address );
}
