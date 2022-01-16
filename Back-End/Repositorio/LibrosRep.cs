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
    public class LibrosRep : ILibrosRep
    {

        /*Inyeccion de dependencias para el contexto */
        private readonly ApplicationDbContext _context;

        public LibrosRep(ApplicationDbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Consulta todos los libro, llamando al procedimiento almacenado 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Libros>> ConsultarAsync()
        {
            List<Libros> libroRespuesta;

            try
            {
                libroRespuesta = await _context.Libros.Include(l => l.IdAutorNavigation).Include(l => l.IdGeneroNavigation).ToListAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return libroRespuesta;
        }

        ///// <summary>
        ///// Consulta La Parametro por Id
        ///// </summary>
        ///// <param name="Id"></param>Parametro de tipo string
        ///// <returns></returns>
        public async Task<Libros> ConsultarPorIdAsync(int id)
        {
            Libros libroRespuesta;

            try
            {
                libroRespuesta = await _context.Libros.Where(x => x.Id == id).SingleOrDefaultAsync<Libros>();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return libroRespuesta;
        }

        public async Task<Libros> ConsultarPorTituloAsync(string titulo)
        {
            Libros libroRespuesta;

            try
            {
                libroRespuesta = await _context.Libros.Where(x => x.Titulo == titulo).SingleOrDefaultAsync<Libros>();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return libroRespuesta;
        }

        public async Task<Libros> Crear(Libros Libros)
        {

            try
            {
                await _context.Libros.AddAsync(Libros);
                await _context.SaveChangesAsync();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return Libros;

        }

        public async Task<bool> Editar(Libros librosEdita)
        {

            Libros LibrosEdita = new Libros();
            bool existeLibro = false;

            try
            {

                LibrosEdita = _context.Libros.Where(x => x.Id == librosEdita.Id).SingleOrDefault<Libros>();

                if (LibrosEdita != null)
                {
                    //llena el objeto Libro
                    LibrosEdita.Titulo = librosEdita.Titulo;
                    LibrosEdita.Ano = librosEdita.Ano;
                    LibrosEdita.IdGenero = librosEdita.IdGenero ;
                    LibrosEdita.NumeroDePaginas = librosEdita.NumeroDePaginas;
                    LibrosEdita.IdAutor = librosEdita.IdAutor;

                    _context.Entry(LibrosEdita).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    existeLibro = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            return existeLibro;

        }

    }


    /*interfas*/
    public interface ILibrosRep
    {
        Task<List<Libros>> ConsultarAsync();
        Task<Libros> ConsultarPorIdAsync(int id);
        Task<Libros> ConsultarPorTituloAsync(string titulo);
        Task<Libros> Crear(Libros Libros);
        Task<bool> Editar(Libros Libros);
    }

}