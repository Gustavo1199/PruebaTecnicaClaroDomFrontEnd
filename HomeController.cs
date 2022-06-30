using ClaroDom.Modelos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClaroDom
{
    public class HomeController : Controller
    {

        static HttpClient client = new HttpClient();

        public async Task<IActionResult> Index(int? Id)
        {
            //if (Id == null)
            //{

                var Lista = await ListaRegistro();

                var FILTRO = Lista.Valores.Where(x => x.id == Id || Id == null).ToList();

            var Values = new ModelPagination();
            Values.Valores = new List<VistaRegistro>();
            Values.Valores = FILTRO;
            ViewBag.ListaRegistro = Values;



            //}
            //else
            //{

            //    var Values = new ModelPagination();
            //    Values.Valores = new List<VistaRegistro>();
            //    Values.Valores.Add(await GetById(Id.Value));
            //    ViewBag.ListaRegistro = Values.Valores;
            //}
            return View();
        }
        public IActionResult Buscar(int Id)
        {
            return RedirectToAction("Index", new { Id = Id }); 
        }

        public async Task<IActionResult> Update(int Id)
        {
            var model = new VistaRegistro();

            var modelReturn = await GetById(Id);

            model = modelReturn;

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create([Bind("title,description,excerpt,publishDate")] VistaRegistro registro)
        {

            if (ModelState.IsValid)
            {
                using (var content = new StringContent(JsonConvert.SerializeObject(registro), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = client.PostAsync("http://clarodom-001-site1.itempurl.com/api/Books/AddBooks", content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        return RedirectToAction("Index");
                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                }
                //var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json"))
                //    HttpResponseMessage response = await client.PostAsync(
                //"https://localhost:44375/api/Books/AddBooks", registro).Result;
                //response.EnsureSuccessStatusCode();
                //return RedirectToAction("Index");
            }

            return View(registro);
        }

        [HttpPost]
        public async Task<ActionResult> Update([Bind("id,title,description,excerpt,publishDate")] VistaRegistro registro)
        {

            if (ModelState.IsValid)
            {
                using (var content = new StringContent(JsonConvert.SerializeObject(registro), System.Text.Encoding.UTF8, "application/json"))
                {
                    HttpResponseMessage result = client.PutAsync($"http://clarodom-001-site1.itempurl.com/api/Books/UpdateBooks/{registro.id}", content).Result;
                    if (result.StatusCode == System.Net.HttpStatusCode.OK)
                        return RedirectToAction("Index");
                    string returnValue = result.Content.ReadAsStringAsync().Result;
                    throw new Exception($"Failed to POST data: ({result.StatusCode}): {returnValue}");
                }
                //var content = new StringContent(JsonConvert.SerializeObject(json), System.Text.Encoding.UTF8, "application/json"))
                //    HttpResponseMessage response = await client.PostAsync(
                //"https://localhost:44375/api/Books/AddBooks", registro).Result;
                //response.EnsureSuccessStatusCode();
                //return RedirectToAction("Index");
            }

            return View(registro);
        }


        [HttpGet]
        public async Task<ActionResult> Detalles(int Id)
        {
            var request = await GetById(Id);
            return View(request);
        }



        private async Task<ModelPagination> ListaRegistro()
        {
            var Values = new ModelPagination();
            Values.Valores = new List<VistaRegistro>();


            //var Values = new ModelPagination();
            ResquestResult result = new ResquestResult();
            Values.Valores = new List<VistaRegistro>();

            HttpResponseMessage response = await client.GetAsync("http://clarodom-001-site1.itempurl.com/api/Books/GetAllBook").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var resulta = response.Content.ReadAsStringAsync().Result;


                var resquest = JsonConvert.DeserializeObject<ResquestResult>(resulta);
                var resquestValores = JsonConvert.DeserializeObject<List<VistaRegistro>>(resquest.result.ToString());

                Values.Valores = resquestValores;

                foreach (var item in Values.Valores)
                {
                    item.descriptiontemp = item.description.Substring(0, 45) + "...";
                    item.excerpttemp = item.excerpt.Substring(0, 45) + "...";
                }
            }
            return Values;

        }

        private async Task<VistaRegistro> GetById(int Id)
        {
            var Values = new VistaRegistro();


            ResquestResult result = new ResquestResult();

            HttpResponseMessage response = await client.GetAsync($"http://clarodom-001-site1.itempurl.com/api/Books/GetBooksById/{Id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var resulta = response.Content.ReadAsStringAsync().Result;


                var resquest = JsonConvert.DeserializeObject<ResquestResult>(resulta);

                Values = JsonConvert.DeserializeObject<VistaRegistro>(resquest.result.ToString());

                //(VistaRegistro)resquest.result;
            }
            return Values;




        }

      
    }
}
