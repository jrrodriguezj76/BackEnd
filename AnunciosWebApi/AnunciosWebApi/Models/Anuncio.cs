using System;
using System.Collections.Generic;

#nullable disable

namespace AnunciosWebApi.Models
{
    public partial class Anuncio
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int IdTipo { get; set; }
        public double Precio { get; set; }
        public byte[] Imagen { get; set; }
    }
}
