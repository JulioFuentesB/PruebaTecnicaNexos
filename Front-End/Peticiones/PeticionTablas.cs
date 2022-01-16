//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;
using Servicios.ClientHttp;
using Front_End.Entidades;
using Newtonsoft.Json;

namespace Servicios.Peticiones.Listados
{
    public class Peticiones : IPeticiones
    {
        private readonly ClienteHttp _client;

        public Peticiones(ClienteHttp client)
        {
            _client = client;
        }

        public async Task<List<Autores>> ConsultarAutoresAsync()
        {

            HttpResponseMessage clientesPeticion;
            List<Autores> _listadoAutores = new List<Autores>();

            try
            {
                clientesPeticion = await _client.client.GetAsync("Autores");

                if (clientesPeticion.IsSuccessStatusCode)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    _listadoAutores = JsonConvert.DeserializeObject<List<Autores>>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return _listadoAutores;
        }

        public async Task<Autores> ConsultarAutorPorIdAsync(int id)
        {

            Autores respuestaPeticion = null;
            HttpResponseMessage clientesPeticion;

            try
            {
                clientesPeticion = await _client.client.GetAsync("Autores/" + id);

                if (clientesPeticion.StatusCode == HttpStatusCode.OK)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    respuestaPeticion = JsonConvert.DeserializeObject<Autores>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return respuestaPeticion;

        }

        public async Task<Autores> ConsultarAutorPorNombreAsync(string nombre)
        {

            Autores respuestaPeticion = null;
            HttpResponseMessage clientesPeticion = null;

            try
            {
                clientesPeticion = await _client.client.GetAsync("Autores/Consultar/" + nombre);

                if (clientesPeticion.StatusCode == HttpStatusCode.OK)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    respuestaPeticion = JsonConvert.DeserializeObject<Autores>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return respuestaPeticion;

        }

        public async Task<bool> CrearAutor(Autores autorCrea)
        {
            HttpResponseMessage respuesta = null;
            try
            {

                string serializaelementoacrear = JsonConvert.SerializeObject(autorCrea);

                respuesta = await _client.client.PostAsync( "Autores",
                    new StringContent(serializaelementoacrear, Encoding.Unicode, "application/json"));

                if (respuesta.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    string contenidoPeticion = respuesta.Content.ReadAsStringAsync().Result;
                    return false;
                }

            }
            catch (Exception Ex)
            {
            }
            return false;

        }

        public async Task<bool> EditarAutor(Autores autorEdita, int id)
        {
            HttpResponseMessage respuesta = null;

            try
            {

                string serializaelementoacrear = JsonConvert.SerializeObject(autorEdita);
                respuesta = await _client.client.PutAsync("Autores/" + id,
                    new StringContent(serializaelementoacrear, Encoding.Unicode, "application/json"));

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        public async Task<List<Libros>> ConsultarLibrosAsync()
        {

            HttpResponseMessage clientesPeticion;
            List<Libros> _listadoLibros = new List<Libros>();

            try
            {
                clientesPeticion = await _client.client.GetAsync("Libros");

                if (clientesPeticion.IsSuccessStatusCode)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    _listadoLibros = JsonConvert.DeserializeObject<List<Libros>>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return _listadoLibros;
        }

        public async Task<Libros> ConsultarLibroPorIdAsync(int id)
        {

            Libros respuestaPeticion = null;
            HttpResponseMessage clientesPeticion;

            try
            {
                clientesPeticion = await _client.client.GetAsync("Libros/" + id);

                if (clientesPeticion.StatusCode == HttpStatusCode.OK)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    respuestaPeticion = JsonConvert.DeserializeObject<Libros>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return respuestaPeticion;

        }

        public async Task<Libros> ConsultarLibroPorTituloAsync(string titulo)
        {

            Libros respuestaPeticion = null;
            HttpResponseMessage clientesPeticion = null;

            try
            {
                clientesPeticion = await _client.client.GetAsync("Libros/Consultar/" + titulo);

                if (clientesPeticion.StatusCode == HttpStatusCode.OK)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    respuestaPeticion = JsonConvert.DeserializeObject<Libros>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return respuestaPeticion;

        }

        public async Task<bool> CrearLibro(Libros creaLibro)
        {
            HttpResponseMessage respuesta = null;
            try
            {

                string serializaelementoacrear = JsonConvert.SerializeObject(creaLibro);

                respuesta = await _client.client.PostAsync( "Libros",
                    new StringContent(serializaelementoacrear, Encoding.Unicode, "application/json"));

                if (respuesta.StatusCode == HttpStatusCode.Created)
                {
                    return true;
                }
                else
                {
                    string contenidoPeticion = respuesta.Content.ReadAsStringAsync().Result;
                    return false;
                }
            }
            catch (Exception Ex)
            {
            }
            return false;

        }

        public async Task<bool> EditarLibro(Libros libroEdita, int id)
        {
            HttpResponseMessage respuesta = null;

            try
            {

                string serializaelementoacrear = JsonConvert.SerializeObject(libroEdita);
                respuesta = await _client.client.PutAsync("Libros/" + id,
                    new StringContent(serializaelementoacrear, Encoding.Unicode, "application/json"));

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception Ex)
            {
                return false;
            }
        }

        public async Task<List<Generos>> ConsultarGenerosAsync()
        {

            HttpResponseMessage clientesPeticion;
            List<Generos> _listadoGeneros = new List<Generos>();

            try
            {
                clientesPeticion = await _client.client.GetAsync("Generos");

                if (clientesPeticion.IsSuccessStatusCode)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    _listadoGeneros = JsonConvert.DeserializeObject<List<Generos>>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return _listadoGeneros;
        }

        public async Task<Generos> ConsultarGeneroPorIdAsync(int id)
        {

            Generos respuestaPeticion = null;
            HttpResponseMessage clientesPeticion;

            try
            {
                clientesPeticion = await _client.client.GetAsync("Generos/" + id);

                if (clientesPeticion.StatusCode == HttpStatusCode.OK)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    respuestaPeticion = JsonConvert.DeserializeObject<Generos>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return respuestaPeticion;

        }

        public async Task<List<Configuraciones>> ConsultarConfiguracionesAsync()
        {

            HttpResponseMessage clientesPeticion;
            List<Configuraciones> _listadoConfiguraciones = new List<Configuraciones>();

            try
            {
                clientesPeticion = await _client.client.GetAsync("Configuraciones");

                if (clientesPeticion.IsSuccessStatusCode)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    _listadoConfiguraciones = JsonConvert.DeserializeObject<List<Configuraciones>>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return _listadoConfiguraciones;
        }

        public async Task<Configuraciones> ConsultarConfiguracionPorIdAsync(int id)
        {

            Configuraciones respuestaPeticion = null;
            HttpResponseMessage clientesPeticion;

            try
            {
                clientesPeticion = await _client.client.GetAsync("Configuraciones/" + id);

                if (clientesPeticion.StatusCode == HttpStatusCode.OK)
                {

                    string contenidoPeticion = clientesPeticion.Content.ReadAsStringAsync().Result;
                    respuestaPeticion = JsonConvert.DeserializeObject<Configuraciones>(contenidoPeticion);
                }
            }
            catch (Exception Ex)
            {
            }

            return respuestaPeticion;

        }

        public async Task<bool> EditarConfiguracion(Configuraciones ConfiguracionEdita, int id)
        {
            HttpResponseMessage respuesta = null;

            try
            {
                string serializaelementoacrear = JsonConvert.SerializeObject(ConfiguracionEdita);
                respuesta = await _client.client.PutAsync("Configuraciones/" + id,
                    new StringContent(serializaelementoacrear, Encoding.Unicode, "application/json"));

                if (respuesta.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
        }

    }

    public interface IPeticiones
    {
        Task<List<Autores>> ConsultarAutoresAsync();
        Task<Autores> ConsultarAutorPorIdAsync(int id);
        Task<Autores> ConsultarAutorPorNombreAsync(string nombre);
        Task<bool> CrearAutor(Autores crea);
        Task<bool> EditarAutor(Autores autorEdita, int id);
        Task<List<Libros>> ConsultarLibrosAsync();
        Task<Libros> ConsultarLibroPorIdAsync(int id);
        Task<Libros> ConsultarLibroPorTituloAsync(string titulo);
        Task<bool> CrearLibro(Libros creaLibro);
        Task<bool> EditarLibro(Libros libroEdita, int id);
        Task<List<Generos>> ConsultarGenerosAsync();
        Task<Generos> ConsultarGeneroPorIdAsync(int id);
        Task<List<Configuraciones>> ConsultarConfiguracionesAsync();
        Task<Configuraciones> ConsultarConfiguracionPorIdAsync(int id);
        Task<bool> EditarConfiguracion(Configuraciones ConfiguracionEdita, int id);
    }
}


