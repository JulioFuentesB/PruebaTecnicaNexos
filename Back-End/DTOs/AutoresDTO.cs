using System;
using System.Collections.Generic;

namespace Back_End.DTOs
{
    public partial class AutoresDTO
    {
        public AutoresDTO()
        {
            Libros = new List<LibrosDTO>();
        }
                
        public int Id { get; set; }     
        public string NombreCompleto { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string CiudadDeProcedencia { get; set; }
        public string CorreoElectronico { get; set; }
        public  List<LibrosDTO> Libros { get; set; }
    }


}
