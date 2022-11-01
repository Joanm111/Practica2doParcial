using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Practica2doParcial.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Telefono = new HashSet<Telefono>();
        }

        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }

        public virtual ICollection<Telefono> Telefono { get; set; }
    }
}
