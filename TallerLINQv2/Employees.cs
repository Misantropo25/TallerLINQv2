using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TallerLINQv2
{
    public partial class Employees
    {
        //Metodo que permite mostrar el nombre completo del empleado y su titulo
        public string EmpleadoRegistrado()
        {
            return TitleOfCourtesy + LastName + " " + FirstName + " - " + Title;
        }

    }
}
