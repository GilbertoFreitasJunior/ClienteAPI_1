using ApiProj.Models;
using ApiWebProj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiWebProj.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient api;
        private Uri baseApiAdress = new("https://localhost:44373/main/Cliente");
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            api = new HttpClient();
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ListaClientes()
        {
            return View(await GetListaClientes());
        }

        public ActionResult AdicionarCliente()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AdicionaCliente(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await api.PostAsJsonAsync(baseApiAdress, cliente);

                    return RedirectToAction(nameof(ListaClientes));
                }
                throw new ArgumentException("Cliente inválido!");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            try
            {
                var clientes = await GetListaClientes();
                if (clientes.Contains(clientes.Where(c => c.Id == id).FirstOrDefault()))
                {
                    await api.DeleteAsync(baseApiAdress + "/" + id);
                    return RedirectToAction(nameof(ListaClientes));
                }

                throw new ArgumentException("Id inválido!");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        [ActionName("GetClienteFone")]
        public async Task<Cliente> GetCliente(int ddd, string fone)
        {
            try
            {
                var response = await api.GetAsync(baseApiAdress + "/" + ddd + "/" + fone);
                var content = await response.Content.ReadAsStringAsync();
                var cliente = JsonConvert.DeserializeObject<Cliente>(content);

                return cliente;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpPost]
        [ActionName("GetClienteCnpj")]
        public async Task<Cliente> GetCliente(string cnpj)
        {
            try
            {
                var cnpjPadrao = new Regex("[0-9]{14}");

                if (cnpjPadrao.Match(cnpj).Success)
                {
                    var response = await api.GetAsync(baseApiAdress + "/" + cnpj);
                    var content = await response.Content.ReadAsStringAsync();
                    var cliente = JsonConvert.DeserializeObject<Cliente>(content);

                    return cliente;
                }

                return null;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        private async Task<List<Cliente>> GetListaClientes()
        {
            try
            {
                var clientes = new List<Cliente>();
                var response = await api.GetAsync(baseApiAdress);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    clientes = JsonConvert.DeserializeObject<List<Cliente>>(content);
                }
                return clientes;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
