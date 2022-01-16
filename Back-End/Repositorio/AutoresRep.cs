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
    public class AutoresRep : IAutoresRep
    {

        /*Inyeccion de dependencias para el contexto */
        private readonly ApplicationDbContext _context;

        public AutoresRep(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Consulta todos los autor, llamando al procedimiento almacenado 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Autores>> ConsultarAsync()
        {
            List<Autores> autorRespuesta;

            try
            {
                autorRespuesta = await _context.Autores.Include(x=>x.Libros).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return autorRespuesta;
        }

        ///// <summary>
        ///// Consulta La Parametro por Id
        ///// </summary>
        ///// <param name="Id"></param>Parametro de tipo string
        ///// <returns></returns>
        public async Task<Autores> ConsultarPorIdAsync(int id)
        {
            Autores autorRespuesta;

            try
            {
                autorRespuesta = await _context.Autores.Where(x => x.Id == id).SingleOrDefaultAsync<Autores>();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return autorRespuesta;
        }

        public async Task<Autores> ConsultarPorNombreAsync(string nombre)
        {
            Autores autorRespuesta;

            try
            {
                autorRespuesta = await _context.Autores.Where(x => x.NombreCompleto == nombre).SingleOrDefaultAsync<Autores>();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return autorRespuesta;
        }

        public async Task<Autores> Crear(Autores autores)
        {

            try
            {
                await _context.Autores.AddAsync(autores);
                await _context.SaveChangesAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return autores;

        }

        public async Task<bool> Editar(Autores autores)
        {

            Autores autoresEdita = new Autores();
            bool existeautor = false;

            try
            {
                autoresEdita = _context.Autores.Where(x => x.Id == autores.Id).SingleOrDefault<Autores>();

                if (autoresEdita != null)
                {
                    //llena el objeto autor
                    autoresEdita.NombreCompleto = autores.NombreCompleto;
                    autoresEdita.FechaNacimiento = autores.FechaNacimiento;
                    autoresEdita.CiudadDeProcedencia = autores.CiudadDeProcedencia;
                    autoresEdita.CorreoElectronico = autores.CorreoElectronico;

                    _context.Entry(autoresEdita).State = EntityState.Modified;
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
    public interface IAutoresRep
    {
        Task<List<Autores>> ConsultarAsync();
        Task<Autores> ConsultarPorIdAsync(int id);
        Task<Autores> ConsultarPorNombreAsync(string nombre);
        Task<Autores> Crear(Autores autores);
        Task<bool> Editar(Autores autores);

    }

}