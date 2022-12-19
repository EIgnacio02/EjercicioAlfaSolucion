using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_API.Controllers
{
    public class AlumnoController : ApiController
    {
        [HttpGet]
        [Route("api/Alumno/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Alumno/GetAllBecas/{IdAlumno}")]
        public IHttpActionResult GetAllBecas(int IdAlumno)
        {
            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();
            ML.Result result = BL.AlumnoBeca.GetById(IdAlumno);
            

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        // POST: api/Alumno
        [HttpPost]
        [Route("api/Alumno/Add")]
        public IHttpActionResult Add([FromBody]ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("api/Alumno/Update/{IdAlumno}")]
        public IHttpActionResult Put(int IdAlumno, [FromBody] ML.Alumno alumno)
        {
            alumno.IdAlumno = IdAlumno;
            ML.Result result = BL.Alumno.Update(alumno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/Alumno/Delete/{IdAlumno}")]
        public IHttpActionResult Delete(int IdAlumno)
        {

            ML.Result result = BL.Alumno.Delete(IdAlumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
