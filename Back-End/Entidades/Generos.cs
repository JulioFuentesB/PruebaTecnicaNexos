using System;
using System.Collections.Generic;

namespace Back_End.Entidades
{
    public partial class Generos
    {
        public Generos()
        {
            Libros = new HashSet<Libros>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
