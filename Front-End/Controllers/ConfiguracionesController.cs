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
    public class ConfiguracionesController : Controller
    {
        private readonly IPeticiones _peticion;

        public ConfiguracionesController(IPeticiones peticion)
        {
            this._peticion = peticion;
        }

        // GET: Configuraciones
        public async Task<ActionResult> Listar()
        {
            return View(await _peticion.ConsultarConfiguracionesAsync());
        }
      
        // GET: Configuraciones/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Listar");
            }
            Configuraciones Configuraciones = await _peticion.ConsultarConfiguracionPorIdAsync(id);
            if (Configuraciones == null)
            {
                return RedirectToAction("Listar");
            }
            return View(Configuraciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(Configuraciones Configuraciones)
        {                  


            if (ModelState.IsValid)
            {
                if (await _peticion.EditarConfiguracion(Configuraciones, Configuraciones.Id ))
                {
                    Configuraciones.RegistroExito = true;
                }
                else
                {
                    Configuraciones.Error = true;
                    Configuraciones.MensajeError = "Errror al registrar datos!";
                }
            }
            else
            {
                  ModelState.AddModelError("", "El registro no fue guardado intente de nuevo.");
            }

            return View(Configuraciones);

        }

 
    }
}
