using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Practica2doParcial.Models
{
    public partial class Planes
    {
        public Planes()
        {
            Telefono = new HashSet<Telefono>();
        }

        public string IdPlan { get; set; }
        public string Descripcion { get; set; }
        public decimal Renta { get; set; }
        public decimal CostoMin { get; set; }

        public virtual ICollection<Telefono> Telefono { get; set; }
    }
}
