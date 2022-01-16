using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front_End.Entidades
{
    public partial class Autores: RepuestaPost
    {
        public Autores()
        {
            Libros = new List<Libros>();
        }
            
        public int Id { get; set; }

        [Display(Name = "Nombre completo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y maximo de {1}.", MinimumLength = 6)]
        [Remote("ValidarNombre", "Autores", AdditionalFields = "Id", ErrorMessage = "El {0} ya existe, por favor ingrese otro.", HttpMethod = "GET")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Ciudad de procedencia")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y maximo de {1}.", MinimumLength = 6)]
        public string CiudadDeProcedencia { get; set; }

        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0}  no es una dirección  válida.")]
        public string CorreoElectronico { get; set; }

        public List<Libros> Libros { get; set; }
    }
}
