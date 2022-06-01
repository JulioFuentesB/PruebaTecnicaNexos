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

    [Route("api/Generos")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        /// <summary>
        /// Inyeccionde dependencia para acceder a los metodods de la interfaz
        /// </summary>
        private readonly IGenerosRep _repositorio;
        private readonly IMapper _mapper;

        public GenerosController(IGenerosRep Repositorio
            , IMapper mapper)
        {
            _repositorio = Repositorio;
            this._mapper = mapper;
        }


        /// <summary>
        /// Obtiene todos los Generos
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ObtenerGeneros")]
        public async Task<ActionResult<List<GenerosDTO>>> Get()
        {
            List<GenerosDTO> listadoGenerosRespuesta;

            try
            {
                List<Generos> GenerosDato = await _repositorio.ConsultarAsync();
                listadoGenerosRespuesta = _mapper.Map<List<GenerosDTO>>(GenerosDato);

                return Ok(listadoGenerosRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Obtiene un genero por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Obtenergenero")]
        public async Task<ActionResult<GenerosDTO>> GetAsync(int id)
        {
            GenerosDTO generoRespuesta;
            try
            {
                Generos generoDato = await _repositorio.ConsultarPorIdAsync(id);

                if (generoDato == null)
                {
                    return NotFound();
                }

                generoRespuesta = _mapper.Map<GenerosDTO>(generoDato);
                return Ok(generoRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }


    }

}
