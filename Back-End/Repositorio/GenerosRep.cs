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
    public class GenerosRep : IGenerosRep
    {

        /*Inyeccion de dependencias para el contexto */
        private readonly ApplicationDbContext _context;
        public GenerosRep(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Consulta todos los genero, llamando al procedimiento almacenado 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Generos>> ConsultarAsync()
        {
            List<Generos> generoRespuesta;

            try
            {
                generoRespuesta = await _context.Generos.ToListAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return generoRespuesta;
        }

        ///// <summary>
        ///// Consulta La Parametro por Id
        ///// </summary>
        ///// <param name="Id"></param>Parametro de tipo string
        ///// <returns></returns>
        public async Task<Generos> ConsultarPorIdAsync(int id)
        {
            Generos generoRespuesta;

            try
            {
                generoRespuesta = await _context.Generos.Where(x => x.Id == id).SingleOrDefaultAsync<Generos>();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return generoRespuesta;
        }


    }


    /*interfas*/
    public interface IGenerosRep
    {
        Task<List<Generos>> ConsultarAsync();
        Task<Generos> ConsultarPorIdAsync(int id); 

    }

}