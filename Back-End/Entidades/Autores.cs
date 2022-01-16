using System;
using System.Collections.Generic;

namespace Back_End.Entidades
{
    public partial class Autores
    {
        public Autores()
        {
            Libros = new HashSet<Libros>();
        }

        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CiudadDeProcedencia { get; set; }
        public string CorreoElectronico { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<Libros> Libros { get; set; }
    }
}
