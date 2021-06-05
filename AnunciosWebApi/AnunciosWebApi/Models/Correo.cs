using System;
using System.Collections.Generic;

#nullable disable

namespace AnunciosWebApi.Models
{
    public partial class Correo
    {
        public int IdCorreo { get; set; }
        public string Nombre { get; set; }
        public string Correo1 { get; set; }
        public string Mensaje { get; set; }
    }
}
