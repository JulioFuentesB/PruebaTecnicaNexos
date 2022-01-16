using Back_End.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Back_End.Repositorio
{

    /*Repositorio, estan los metodos, cada metodo posee un procedimiento almacenado  */
    public class ConfiguracionesRep : IConfiguracionesRep
    {

        /*Inyeccion de dependencias para el contexto */
        private readonly ApplicationDbContext _context;
        public ConfiguracionesRep(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Configuraciones>> ConsultarAsync()
        {
            List<Configuraciones> ConfiguracionRespuesta;

            try
            {
                ConfiguracionRespuesta = await _context.Configuraciones.ToListAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return ConfiguracionRespuesta;
        }
 
        public async Task<Configuraciones> ConsultarPorIdAsync(int id)
        {
            Configuraciones ConfiguracionRespuesta;

            try
            {
                ConfiguracionRespuesta = await _context.Configuraciones.Where(x => x.Id == id).SingleOrDefaultAsync<Configuraciones>();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return ConfiguracionRespuesta;
        }

        public async Task<bool> Editar(Configuraciones configuracion)
        {

            Configuraciones configuracionEdita = new Configuraciones();
            bool existeautor = false;

            try
            {
                configuracionEdita = _context.Configuraciones.Where(x => x.Id == configuracion.Id).SingleOrDefault<Configuraciones>();

                if (configuracionEdita != null)
                {
                    //llena el objeto autor
                    configuracionEdita.NumeroLibrosPermitido = configuracion.NumeroLibrosPermitido;     

                    _context.Entry(configuracionEdita).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    existeautor = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return existeautor;
        }

    }


    /*interfas*/
    public interface IConfiguracionesRep
    {
        Task<List<Configuraciones>> ConsultarAsync();
        Task<Configuraciones> ConsultarPorIdAsync(int id);
        Task<bool> Editar(Configuraciones configuracion);
    }

}