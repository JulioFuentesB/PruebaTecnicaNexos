using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front_End.Entidades
{
    public partial class Configuraciones : RepuestaPost
    {
        public int Id { get; set; }

        [Display(Name = "Numero de libros permitidos en el sistema")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, 1000, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int NumeroLibrosPermitido { get; set; }
    }
}
