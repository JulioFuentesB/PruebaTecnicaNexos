using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Front_End.Entidades
{
    public partial class Libros: RepuestaPost
    {
        public Libros()
        {
            Autores = new List<Autores>();
            Generos = new List<Generos>();
        }

        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(200, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2} y maximo de {1}.", MinimumLength = 6)]
        [Remote("ValidarTitulo", "Libros", AdditionalFields = "Id", ErrorMessage = "El {0} ya existe, por favor ingrese otro.", HttpMethod = "GET")]
        public string Titulo { get; set; }

        [Display(Name = "Año")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1000, 2025, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int Ano { get; set; }

        [Display(Name = "Genero")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, 100, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int IdGenero { get; set; }

        [Display(Name = "Numero de pagina")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, int.MaxValue, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int NumeroDePaginas { get; set; }

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Column(TypeName = "numeric")]
        [Range(1, int.MaxValue, ErrorMessage = "El {0} debe estar {1} y {2}.")]
        public int IdAutor { get; set; }
        public bool? Estado { get; set; }
        public Autores IdAutorNavigation { get; set; }
        public Generos IdGeneroNavigation { get; set; }
        public List<Autores> Autores { get; set; }
        public List<Generos> Generos { get; set; }

    }
}
