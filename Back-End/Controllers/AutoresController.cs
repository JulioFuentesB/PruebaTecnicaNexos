using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Back_End.DTOs;
using Back_End.Repositorio;
using AutoMapper;
using Back_End.Entidades;

namespace Back_End.Controllers
{

    [Route("api/Autores")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        /// <summary>
        /// Inyeccionde dependencia para acceder a los metodods de la interfaz
        /// </summary>
        private readonly IAutoresRep _repositorio;
        private readonly IMapper _mapper;

        public AutoresController(IAutoresRep Repositorio
            , IMapper mapper)
        {
            _repositorio = Repositorio;
            this._mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los Autores
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ObtenerAutores")]
        public async Task<ActionResult<List<AutoresDTO>>> Get()
        {
            List<AutoresDTO> listadoAutoresRespuesta;

            try
            {
                List<Autores> autoresDato = await _repositorio.ConsultarAsync();
                listadoAutoresRespuesta = _mapper.Map<List<AutoresDTO>>(autoresDato);
                return Ok(listadoAutoresRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
 
        [HttpGet("{id}", Name = "ObtenerAutor")]
        public async Task<ActionResult<AutoresDTO>> GetAsync(int id)
        {
            AutoresDTO autorRespuesta;
            try
            {
                Autores autorDato = await _repositorio.ConsultarPorIdAsync(id);

                if (autorDato == null)
                {
                    return NotFound();
                }

                autorRespuesta = _mapper.Map<AutoresDTO>(autorDato);
                return Ok(autorRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
                
        [HttpGet("Consultar/{nombre}")]
        public async Task<ActionResult<AutoresDTO>> GetAsync(string nombre)
        {

            AutoresDTO autorRespuesta;
            try
            {
                Autores autorDato = await _repositorio.ConsultarPorNombreAsync(nombre);

                if (autorDato == null)
                {
                    return NotFound();
                }

                autorRespuesta = _mapper.Map<AutoresDTO>(autorDato);
                return Ok(autorRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Crea una Autor
        /// </summary>
        /// <param name="autorCrea"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<AutoresDTO>> Post([FromBody] AutoresCreaDTO autorCrea)
        {
            try
            {

                if ((autorCrea == null) || (!ModelState.IsValid))
                {
                    return BadRequest(ModelState);
                }

                if (await _repositorio.ConsultarPorNombreAsync(autorCrea.NombreCompleto) != null)
                {
                    ModelState.AddModelError(nameof(autorCrea.NombreCompleto), "la Autor con el nombre " + autorCrea.NombreCompleto + ", ya existe.");
                    return BadRequest(ModelState);
                }

                Autores autor = _mapper.Map<Autores>(autorCrea);
                autor = await _repositorio.Crear(autor);
                autorCrea.Id = autor.Id;

                return CreatedAtRoute(null, new { id = autorCrea.Id }, autorCrea);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Actualiza una Autor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autorEdita"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<AutoresDTO>> Put(int id, [FromBody] AutoresCreaDTO autorEdita)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != autorEdita.Id || autorEdita.Id < 1)
                {
                    ModelState.AddModelError(nameof(autorEdita.Id), "El campo " + nameof(autorEdita.Id) + " es obligatorio y mayor a 0.");
                    return BadRequest(ModelState);
                }

                Autores autorDato = await _repositorio.ConsultarPorNombreAsync(autorEdita.NombreCompleto);

                if (autorDato != null && autorDato.Id != autorEdita.Id)
                {
                    ModelState.AddModelError(nameof(autorEdita.NombreCompleto), "El Autor con el nombre : " + autorEdita.NombreCompleto + ", ya existe.");
                    return BadRequest(ModelState);
                }

                Autores autor = _mapper.Map<Autores>(autorEdita);
                bool respuestaAutorEditajoDato = await _repositorio.Editar(autor);

                if (!respuestaAutorEditajoDato)
                {
                    return NotFound();
                }

                return Ok(autorEdita);

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }

}
