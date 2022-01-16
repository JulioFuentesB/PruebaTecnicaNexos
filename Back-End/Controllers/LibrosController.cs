using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.DTOs;
using Back_End.Repositorio;
using AutoMapper;
using Back_End.Entidades;
using System.Linq;

namespace Back_End.Controllers
{

    [Route("api/Libros")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        /// <summary>
        /// Inyeccionde dependencia para acceder a los metodods de la interfas
        /// </summary>
        private readonly ILibrosRep _repositorio;
        private readonly IMapper _mapper;
        private readonly IAutoresRep _autoresRep;
        private readonly IConfiguracionesRep _configuracionesRep;

        public LibrosController(ILibrosRep Repositorio
            , IMapper mapper
            , IAutoresRep  autoresRep
            , IConfiguracionesRep configuracionesRep)
        {
            _repositorio = Repositorio;
            this._mapper = mapper;
            this._autoresRep = autoresRep;
            this._configuracionesRep = configuracionesRep;
        }

        /// <summary>
        /// Obtiene todos los Libros
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ObtenerLibros")]
        public async Task<ActionResult<List<LibrosDTO>>> Get()
        {
            List<LibrosDTO> listadoLibrosRespuesta;

            try
            {
                List<Libros> LibrosDato = await _repositorio.ConsultarAsync();
                listadoLibrosRespuesta = _mapper.Map<List<LibrosDTO>>(LibrosDato);

                return Ok(listadoLibrosRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Obtiene un libro por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Obtenerlibro")]
        public async Task<ActionResult<LibrosDTO>> GetAsync(int id)
        {
            LibrosDTO libroRespuesta;
            try
            {
                Libros libroDato = await _repositorio.ConsultarPorIdAsync(id);

                if (libroDato == null)
                {
                    return NotFound();
                }

                libroRespuesta = _mapper.Map<LibrosDTO>(libroDato);
                return Ok(libroRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        ///// <summary>
        ///// Obtiene un libro por nombre
        ///// </summary>
        ///// <param name="nombre"></param>
        ///// <returns></returns>
        [HttpGet("Consultar/{nombre}")]
        public async Task<ActionResult<LibrosDTO>> GetAsync(string nombre)
        {

            LibrosDTO libroRespuesta;
            try
            {
                Libros libroDato = await _repositorio.ConsultarPorTituloAsync(nombre);

                if (libroDato == null)
                {
                    return NotFound();
                }

                libroRespuesta = _mapper.Map<LibrosDTO>(libroDato);
                return Ok(libroRespuesta);

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Crea una libro
        /// </summary>
        /// <param name="libroCrea"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LibrosDTO>> Post([FromBody] LibrosDTO libroCrea)
        {
            try
            {
                int cantidadRegistrosPermitidos = 0;
                int cantidaLibros = 0;

                if (libroCrea == null)
                {
                    return BadRequest(ModelState);
                }

                if (await _repositorio.ConsultarPorTituloAsync(libroCrea.Titulo) != null)
                {
                    ModelState.AddModelError(nameof(libroCrea.Titulo), "la libro con el nombre " + libroCrea.Titulo + ", ya existe.");
                    return BadRequest(ModelState);
                }

                List<Configuraciones> configuraciones = await _configuracionesRep.ConsultarAsync();

                if (configuraciones.Count() > 0)
                {
                    cantidadRegistrosPermitidos = configuraciones.FirstOrDefault().NumeroLibrosPermitido;
                    var  libros = await _repositorio.ConsultarAsync();
                    cantidaLibros = libros.Count();
                }

                if (cantidadRegistrosPermitidos!= 0 && cantidaLibros!= 0 && (cantidaLibros >= cantidadRegistrosPermitidos ))
                {
                    ModelState.AddModelError("", "El libro no se puede guardar, excede el limite permitido, ya existen,"+ (cantidaLibros)+", limite de libros configurado es de "+ cantidadRegistrosPermitidos);
                }

                if (await _autoresRep.ConsultarPorIdAsync(libroCrea.IdAutor) == null)
                {
                    ModelState.AddModelError(nameof(libroCrea.IdAutor), "El autor no está registrado.");                 
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Libros libro = _mapper.Map<Libros>(libroCrea);
                libro = await _repositorio.Crear(libro);
                libroCrea.Id = libro.Id;

                return CreatedAtRoute(null, new { id = libroCrea.Id }, libroCrea);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Actualiza una libro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="libroEdita"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<LibrosDTO>> Put(int id, [FromBody] LibrosDTO libroEdita)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != libroEdita.Id || libroEdita.Id < 1)
                {
                    ModelState.AddModelError(nameof(libroEdita.Id), "El campo " + nameof(libroEdita.Id) + " es obligatorio y mayor a 0.");
                    return BadRequest(ModelState);
                }

                if (await _autoresRep.ConsultarPorIdAsync(libroEdita.IdAutor) == null)
                {
                    ModelState.AddModelError(nameof(libroEdita.IdAutor), "El autor no está registrado.");
                    return BadRequest(ModelState);
                }

                Libros libroDato = await _repositorio.ConsultarPorTituloAsync(libroEdita.Titulo);

                if (libroDato != null && libroDato.Id != libroEdita.Id)
                {
                    ModelState.AddModelError(nameof(libroEdita.Titulo), "El libro con el nombre : " + libroEdita.Titulo + ", ya existe.");
                    return BadRequest(ModelState);
                }

                Libros libro = _mapper.Map<Libros>(libroEdita);
                bool respuestalibroEditajoDato = await _repositorio.Editar(libro);

                if (!respuestalibroEditajoDato)
                {
                    return NotFound();
                }

                return Ok(libroEdita);

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }

}
