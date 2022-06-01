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

    [Route("api/Configuraciones")]
    [ApiController]
    public class ConfiguracionesController : ControllerBase
    {
        /// <summary>
        /// Inyeccionde dependencia para acceder a los metodods de la interfaz
        /// </summary>
        private readonly IConfiguracionesRep _repositorio;
        private readonly IMapper _mapper;

        public ConfiguracionesController(IConfiguracionesRep Repositorio
            , IMapper mapper)
        {
            _repositorio = Repositorio;
            this._mapper = mapper;
        }

        /// <summary>
        /// Obtiene todos los Configuraciones
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "ObtenerConfiguraciones")]
        public async Task<ActionResult<List<ConfiguracionesDTO>>> Get()
        {
            List<ConfiguracionesDTO> listadoConfiguracionesRespuesta;

            try
            {
                List<Configuraciones> ConfiguracionesDato = await _repositorio.ConsultarAsync();
                listadoConfiguracionesRespuesta = _mapper.Map<List<ConfiguracionesDTO>>(ConfiguracionesDato);

                return Ok(listadoConfiguracionesRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Obtiene un Configuracion por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "ObtenerConfiguracion")]
        public async Task<ActionResult<ConfiguracionesDTO>> GetAsync(int id)
        {
            ConfiguracionesDTO ConfiguracionRespuesta;
            try
            {
                Configuraciones ConfiguracionDato = await _repositorio.ConsultarPorIdAsync(id);

                if (ConfiguracionDato == null)
                {
                    return NotFound();
                }

                ConfiguracionRespuesta = _mapper.Map<ConfiguracionesDTO>(ConfiguracionDato);
                return Ok(ConfiguracionRespuesta);
            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AutoresDTO>> Put(int id, [FromBody] ConfiguracionesDTO configuracionEdita)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != configuracionEdita.Id || configuracionEdita.Id < 1)
                {
                    ModelState.AddModelError(nameof(configuracionEdita.Id), "El campo " + nameof(configuracionEdita.Id) + " es obligatorio y mayor a 0.");
                    return BadRequest(ModelState);
                }

                Configuraciones autor = _mapper.Map<Configuraciones>(configuracionEdita);
                bool respuestaAutorEditajoDato = await _repositorio.Editar(autor);

                if (!respuestaAutorEditajoDato)
                {
                    return NotFound();
                }

                return Ok(configuracionEdita);

            }
            catch (Exception Ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }

}
