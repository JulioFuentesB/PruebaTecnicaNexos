using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.DTOs
{
    public class LibrosDTO
    {

        [Column(TypeName = "numeric")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y maximo de {1}.", MinimumLength = 6)]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1000, 2025, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int Ano { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, 100, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int IdGenero { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, int.MaxValue, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int NumeroDePaginas { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, int.MaxValue, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int IdAutor { get; set; }
        public bool? Estado { get; set; }
        public  AutoresDTO IdAutorNavigation { get; set; }
        public  GenerosDTO IdGeneroNavigation { get; set; }
    }


}
