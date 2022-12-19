using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class AlumnoBecasController : Controller
    {
        // GET: AlumnoBecas
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            ML.Alumno alumno = new ML.Alumno();
            ML.Beca beca = new ML.Beca();
            ML.Result result = BL.AlumnoBeca.GetAll();
            if (result.Correct)
            {
                alumnoBeca.AlumnoBecas = result.Objects;
                return View(alumnoBeca);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetAllBecas(int? IdAlumno)
        {
            ML.Result result = BL.AlumnoBeca.GetById(IdAlumno.Value);
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            ML.Result resultAlumno = BL.Alumno.GetById(IdAlumno.Value);
            if (result.Correct)
            {
                alumnoBeca.AlumnoBecas = result.Objects;
                alumnoBeca.Alumno = (ML.Alumno)resultAlumno.Object;

                return View(alumnoBeca);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al consultar el alummno seleccionado";
                return View();
            }
        }
    }
}