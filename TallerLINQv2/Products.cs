using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerLINQv2
{
    public partial class Products
    {
        //Metodo que permite mostrar el nombre del producto y su stock
        public string ProductoDisponible()
        {
            return ProductName + " - " + UnitsInStock;
        }

    }
}
