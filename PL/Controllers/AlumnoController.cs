using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult GetAll()
        {
            ML.Alumno alumno = new ML.Alumno();
            //ML.Result result = BL.Alumno.GetAll();
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    //ConfigurationManager.AppSettings["webApiUrl"]
                    client.BaseAddress = new Uri("http://localhost:60672/");
                    var responseTask = client.GetAsync("api/Alumno/GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Alumno>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
            }
            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                return View(alumno);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetAllBecas(int? IdAlumno)
        {
            ML.Result result = new ML.Result();
            ML.Result resultAlumno =new ML.Result();
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            //ML.Result result = BL.AlumnoBeca.GetById(IdAlumno.Value);
            //ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno.Value);
            result.Objects = new List<object>();
            try
            {
                using (var client = new HttpClient())
                {
                    //ConfigurationManager.AppSettings["webApiUrl"]
                    client.BaseAddress = new Uri("http://localhost:60672/");
                    var responseTask = client.GetAsync("api/Alumno/GetAllBecas/" + IdAlumno);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.AlumnoBeca resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.AlumnoBeca>(resultItem.ToString());
                            result.Objects.Add(resultItemList);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
            }
            if (result.Correct)
            {

                alumnoBeca.AlumnoBecas = result.Objects;
                alumnoBeca.Alumno = (ML.Alumno)resultAlumno.Object;

            }
            return View(alumnoBeca);
        }



        [HttpGet]
        public ActionResult Form(int? IdAlumno)
        {
            ML.Alumno alumno = new ML.Alumno();
            if (IdAlumno==null)
            {
                return View(alumno);
            }
            else
            {
                ML.Result result = BL.Alumno.GetById(IdAlumno.Value);
                if (result.Correct)
                {
                    alumno = (ML.Alumno)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el alummno seleccionado";
                }
                return View(alumno);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            if (alumno.IdAlumno == 0)
            {
                //result = BL.Alumno.Add(alumno);


                result.Objects = new List<object>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        //ConfigurationManager.AppSettings["webApiUrl"]
                        client.BaseAddress = new Uri("http://localhost:60672/");
                        var responseTask = client.PostAsJsonAsync<ML.Alumno>("api/Alumno/Add/",alumno);
                        responseTask.Wait();

                        var resultServicio = responseTask.Result;

                        if (resultServicio.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "El alumno se registro correctamente";
                        }
                        else
                        {
                            ViewBag.Mensaje = "El alumno no se ha registrado correctamente" + result.Message;
                        }

                    }
                }
                catch (Exception ex)
                {
                    result.Ex = ex;
                }

                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                }
                else
                {
                    ViewBag.Mensaje = "No ha registrado el alumno" + result.Message;
                }
            }
            else
            {
                //result = BL.Alumno.Update(alumno);

                result.Objects = new List<object>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        //ConfigurationManager.AppSettings["webApiUrl"]
                        client.BaseAddress = new Uri("http://localhost:60672/");
                        var responseTask = client.PostAsJsonAsync<ML.Alumno>("api/Alumno/Update/" + alumno.IdAlumno, alumno);
                        responseTask.Wait();
                        var resultServicio = responseTask.Result;
                    }
                }
                catch (Exception ex)
                {
                    result.Ex = ex;
                }

                if (result.Correct)
                {
                }
                else
                {
                    ViewBag.Mensaje = "No ha podido actualizar los datos " + result.Message;
                }
            }
            return PartialView("Modal");

        }

        [HttpGet]
        public ActionResult Delete(int? IdAlumno)
        {
            ML.Result result = new ML.Result();
            if (IdAlumno >= 1)
            {
                //result = BL.Alumno.Delete(IdAlumno.Value);
                result.Objects = new List<object>();
                try
                {
                    using (var client = new HttpClient())
                    {
                        //ConfigurationManager.AppSettings["webApiUrl"]
                        client.BaseAddress = new Uri("http://localhost:60672/");
                        var responseTask = client.DeleteAsync("api/Alumno/Delete/" + IdAlumno);
                        responseTask.Wait();
                        var resultServicio = responseTask.Result;
                        if (resultServicio.IsSuccessStatusCode)
                        {
                            ViewBag.Mensaje = "El alumno se elimino correctamente";
                        }
                        else
                        {
                            ViewBag.Mensaje = "El alumno no se ha elimino correctamente" + result.Message;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Ex = ex;
                }

                ViewBag.Message = result.Message;
            }
            else
            {
                ViewBag.Mensaje = "No ha registrado el alumno" + result.Message;
            }
            return PartialView("Modal");

        }
    }
}