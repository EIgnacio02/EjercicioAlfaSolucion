using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class AlumnoBeca
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {

                    var query = (from AlumnoBeca in context.AlumnoBecas
                                 join Becas in context.Becas on AlumnoBeca.IdBeca equals Becas.IdBeca 
                                 join Alumnos in context.Alumnoes on AlumnoBeca.IdAlumno equals Alumnos.IdAlumno
                                 select new
                                 {
                                     IdAlumnoBeca = AlumnoBeca.IdAlumnoBeca,
                                     IdBeca = AlumnoBeca.IdBeca,
                                     NombreBeca=AlumnoBeca.Beca.Nombre,
                                     IdAlumno = AlumnoBeca.IdAlumno,
                                     NombreAlumno = AlumnoBeca.Alumno.Nombre
                                 });

                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();

                            alumnoBeca.IdAlumnoBeca = obj.IdAlumnoBeca;
                            alumnoBeca.Alumno = new ML.Alumno();
                            alumnoBeca.Alumno.IdAlumno = (int)obj.IdAlumno;
                            alumnoBeca.Alumno.Nombre = obj.NombreAlumno;
                            alumnoBeca.Beca = new ML.Beca();

                            alumnoBeca.Beca.IdBeca = (int)obj.IdBeca;
                            alumnoBeca.Beca.Nombre = obj.NombreBeca;
                            result.Objects.Add(alumnoBeca);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un erro";
            }
            return result;
        }

        public static ML.Result GetById(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {
                    var query = (from AlumnoBeca in context.AlumnoBecas
                                 join BecasNom in context.Becas on AlumnoBeca.IdBeca equals BecasNom.IdBeca
                                 join Alumno in context.Alumnoes on AlumnoBeca.IdAlumno equals Alumno.IdAlumno
                                 where AlumnoBeca.IdAlumno == IdAlumno
                                 select new
                                 {
                                     IdAlumnoBeca = AlumnoBeca.IdAlumnoBeca,
                                     IdBeca = AlumnoBeca.IdBeca,
                                     NombreBeca = AlumnoBeca.Beca.Nombre,
                                     IdAlumno = AlumnoBeca.IdAlumno, 
                                     NombreAlumno = AlumnoBeca.Alumno.Nombre
                                 });

                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.AlumnoBeca alumnoBeca = new ML.AlumnoBeca();

                            alumnoBeca.IdAlumnoBeca = obj.IdAlumnoBeca;

                            alumnoBeca.Alumno = new ML.Alumno();
                            alumnoBeca.Alumno.IdAlumno = (int)obj.IdAlumno;
                            alumnoBeca.Alumno.Nombre = obj.NombreAlumno;
                            alumnoBeca.Beca = new ML.Beca();

                            alumnoBeca.Beca.IdBeca = (int)obj.IdBeca;
                            alumnoBeca.Beca.Nombre = obj.NombreBeca;
                            result.Objects.Add(alumnoBeca);
                        }

                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un erro";
            }
            return result;
        }
    }
}
