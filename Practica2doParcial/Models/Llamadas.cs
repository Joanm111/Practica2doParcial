using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Practica2doParcial.Models
{
    public partial class Llamadas
    {
        public int CodLlamada { get; set; }
        public string Telefono { get; set; }
        public string Fecha { get; set; }
        public int Duracion { get; set; }

        public virtual Telefono TelefonoNavigation { get; set; }
    }
}
