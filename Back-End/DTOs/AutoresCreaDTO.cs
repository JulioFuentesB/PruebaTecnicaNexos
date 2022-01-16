using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.DTOs
{
    public partial class AutoresCreaDTO
    {
        
        [Column(TypeName = "numeric")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y maximo de {1}.", MinimumLength = 6)]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y maximo de {1}.", MinimumLength = 6)]
        public string CiudadDeProcedencia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0}  no es una dirección  válida.")]
        public string CorreoElectronico { get; set; }
        
    }


}
