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
    public class AutoresController : Controller
    {
        private readonly IPeticiones _peticion;

        public AutoresController(IPeticiones peticion)
        {
            this._peticion = peticion;
        }

        // GET: Autores
        public async Task<ActionResult> Listar()
        {
            return View(await _peticion.ConsultarAutoresAsync());
        }

        // GET: Autores/Details/5
        public async Task<ActionResult> Detalles(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Listar");
            }
            Autores autores = await _peticion.ConsultarAutorPorIdAsync(id);
            if (autores == null)
            {
                return RedirectToAction("Listar");
            }
            return View(autores);
        }

        // GET: Autores/Create
        public async Task<ActionResult> Crear()
        {
            Autores autores = new Autores();

            return View(autores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(Autores autores)
        {
            if (ModelState.IsValid)
            {
                if (await _peticion.CrearAutor(autores))
                {
                    autores.RegistroExito = true;
                }
                else
                {
                     autores.Error = true;
                     autores.MensajeError = "Errror al registrar datos!";
                }
            }
            else
            {
                ModelState.AddModelError("", "El cliente no fue creado intente de nuevo.");
            }

            return View(autores);
        }

        // GET: Autores/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Listar");
            }
            Autores autores = await _peticion.ConsultarAutorPorIdAsync(id);
            if (autores == null)
            {
                return RedirectToAction("Listar");
            }
            return View(autores);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Autores autores)
        {                 

            if (ModelState.IsValid)
            {
                if (await _peticion.EditarAutor(autores, autores.Id ))
                {
                    autores.RegistroExito = true;
                }
                else
                {
                    autores.Error = true;
                    autores.MensajeError = "Errror al registrar datos!";
                }
            }
            else
            {
                  ModelState.AddModelError("", "El registro no fue guardado intente de nuevo.");
            }

            return View(autores);

        }

        ///// <summary>
        ///// Validacion personalizada con DataAnnotations remote
        ///// </summary>
        ///// <param name="NombreCompleto"></param>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ValidarNombre(string NombreCompleto, int? Id)
        {
            try
            {

                Autores autorRespuesta = await _peticion.ConsultarAutorPorNombreAsync(NombreCompleto);

                if (autorRespuesta == null)
                {
                    return Json(true);
                }

                if (Id != 0 && autorRespuesta != null && autorRespuesta.Id == Id)
                {
                    return Json(true);
                }

                return Json("La autor con el nombre : " + NombreCompleto + " ya existe, por favor ingrese otra.");

            }
            catch (Exception)
            {
                return Json(false);
            }

        }

 
    }
}
