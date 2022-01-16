using System;
using System.Collections.Generic;

namespace Back_End.Entidades
{
    public partial class Libros
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public int IdGenero { get; set; }
        public int NumeroDePaginas { get; set; }
        public int IdAutor { get; set; }
        public bool? Estado { get; set; }

        public virtual Autores IdAutorNavigation { get; set; }
        public virtual Generos IdGeneroNavigation { get; set; }
    }
}
