using Front_End.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.Peticiones.Listados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Front_End.Controllers
{
    public class LibrosController : Controller
    {
        private readonly IPeticiones _peticion;

        public LibrosController(IPeticiones peticion)
        {
            this._peticion = peticion;
        }

        // GET: Libros
        public async Task<ActionResult> Listar()
        {
            return View(await _peticion.ConsultarLibrosAsync());
        }

        // GET: Libros/Details/5
        public async Task<ActionResult> Detalles(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Listar");
            }
            Libros Libros = await _peticion.ConsultarLibroPorIdAsync(id);
            if (Libros == null)
            {
                return RedirectToAction("Listar");
            }
            return View(Libros);
        }

        // GET: Libros/Create
        public async Task<ActionResult> Crear()
        {
            Libros Libros = new Libros();
            await CaragarListadosAsync(Libros);

            return View(Libros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(Libros Libros)
        {
            int cantidadRegistrosPermitidos = 0;
            int cantidaLibros = 0;
            List<Configuraciones> configuraciones = await _peticion.ConsultarConfiguracionesAsync();

            if (configuraciones.Count() > 0)
            {
                cantidadRegistrosPermitidos = configuraciones.FirstOrDefault().NumeroLibrosPermitido;
                var libros = await _peticion.ConsultarLibrosAsync();
                cantidaLibros = libros.Count();
            }

            if (cantidadRegistrosPermitidos != 0 && cantidaLibros != 0 && (cantidaLibros >= cantidadRegistrosPermitidos))
            {
                ModelState.AddModelError("", "El libro no se puede guardar, excede el limite permitido, cantidad libros :" + (cantidaLibros) + ", limite de libros configurado : " + cantidadRegistrosPermitidos);
            }

            if (ModelState.IsValid)
            {
                if (await _peticion.CrearLibro(Libros))
                {
                    Libros.RegistroExito = true;
                }
                else
                {
                     Libros.Error = true;
                     Libros.MensajeError = "Errror al registrar datos!";
                }
            }
            else
            {
                await CaragarListadosAsync(Libros);
                ModelState.AddModelError("", "El cliente no fue creado intente de nuevo.");
            }

            return View(Libros);
        }

        // GET: Libros/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Listar");
            }
            Libros Libros = await _peticion.ConsultarLibroPorIdAsync(id);
            if (Libros == null)
            {
                return RedirectToAction("Listar");
            }

            await CaragarListadosAsync(Libros);

            return View(Libros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Libros Libros)
        {                  


            if (ModelState.IsValid)
            {
                if (await _peticion.EditarLibro(Libros, Libros.Id ))
                {
                    Libros.RegistroExito = true;
                }
                else
                {
                    Libros.Error = true;
                    Libros.MensajeError = "Errror al registrar datos!";
                }
            }
            else
            {
                await CaragarListadosAsync(Libros);
                  ModelState.AddModelError("", "El registro no fue guardado intente de nuevo.");
            }

            return View(Libros);

        }

        ///// <summary>
        ///// Validacion personalizada con DataAnnotations remote
        ///// </summary>
        ///// <param name="NombreCompleto"></param>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ValidarTitulo(string Titulo, int? Id)
        {
            try
            {

                Libros libroRespuesta = await _peticion.ConsultarLibroPorTituloAsync(Titulo);

                if (libroRespuesta == null)
                {
                    return Json(true);
                }

                if (Id != 0 && libroRespuesta != null && libroRespuesta.Id == Id)
                {
                    return Json(true);
                }

                return Json("La autor con el nombre : " + Titulo + " ya existe, por favor ingrese otra.");

            }
            catch (Exception)
            {
                return Json(false);
            }

        }

        private async Task CaragarListadosAsync(Libros libro)
        {
            try
            {
                //peticiones asincronas a listados 
                Task<List<Autores>> autores = _peticion.ConsultarAutoresAsync();
                Task<List<Generos>> generos = _peticion.ConsultarGenerosAsync();

                //espera las peticiones de los listados como si fuera en cada una un await
                await Task.WhenAll(
                            autores,
                            generos
                 );

                //carga el resultado de peticiones asincronas
                libro.Autores = autores.Result;
                libro.Generos = generos.Result;

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
