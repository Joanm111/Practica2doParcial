using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Practica2doParcial.Models
{
    public partial class Telefono
    {
        public Telefono()
        {
            Llamadas = new HashSet<Llamadas>();
        }

        public string Telefono1 { get; set; }
        public int IdCliente { get; set; }
        public string TipoPlan { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; }
        public virtual Planes TipoPlanNavigation { get; set; }
        public virtual ICollection<Llamadas> Llamadas { get; set; }
    }
}
