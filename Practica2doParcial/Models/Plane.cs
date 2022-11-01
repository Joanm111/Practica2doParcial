using System;
using System.Collections.Generic;

namespace Practica2doParcial.Models
{
    public class Plane
    {
        public Plane()
        {
            Telefonos = new HashSet<Telefono>();
        }

        public string IdPlan { get; set; } 
        public string Descripcion { get; set; } 
        public decimal Renta { get; set; }
        public decimal CostoMin { get; set; }

        public virtual ICollection<Telefono> Telefonos { get; set; }
    }
}
