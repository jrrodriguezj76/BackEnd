using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnunciosWebApi.DataTransferO
{
    public class AnuncioView
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public double Precio { get; set; }
        public byte[] Imagen { get; set; }
    }
}
