using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Alumno
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context =new DL.AlfaSolutionEntities())
                {
                    var query = (from Alumno in context.Alumnoes
                                 select new
                                 {
                                     IdAlumno=Alumno.IdAlumno,
                                     Nombre=Alumno.Nombre,
                                     Genero=Alumno.Genero,
                                     Edad=Alumno.Edad
                                 });

                    if (query != null && query.ToList().Count>0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Alumno alumno = new ML.Alumno();

                            alumno.IdAlumno = obj.IdAlumno;
                            alumno.Nombre = obj.Nombre;
                            alumno.Genero = (bool)obj.Genero;
                            alumno.Edad = (int)obj.Edad;

                            result.Objects.Add(alumno);
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
                    var query = (from Alumno in context.Alumnoes
                                 where Alumno.IdAlumno == IdAlumno  
                                 select new
                                 {
                                     IdAlumno = Alumno.IdAlumno,
                                     Nombre = Alumno.Nombre,
                                     Genero = Alumno.Genero,
                                     Edad = Alumno.Edad
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        ML.Alumno alumno = new ML.Alumno();

                        alumno.IdAlumno = query.IdAlumno;
                        alumno.Nombre = query.Nombre;
                        alumno.Genero = (bool)query.Genero;
                        alumno.Edad = (int)query.Edad;

                        result.Object = alumno;
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

        public static ML.Result Add(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {

                    DL.Alumno alumnoLINQ =new DL.Alumno();

                    alumnoLINQ.Nombre = alumno.Nombre;
                    alumnoLINQ.Genero = (bool)alumno.Genero;
                    alumnoLINQ.Edad = (int)alumno.Edad;
                    
                    context.Alumnoes.Add(alumnoLINQ);
                    context.SaveChanges();

                    result.Message = "Se ingresaron los datos correctamente";
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

        public static ML.Result Update(ML.Alumno alumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {
                    var query = (from Alumno in context.Alumnoes
                                 where Alumno.IdAlumno == alumno.IdAlumno
                                 select Alumno).SingleOrDefault();

                    if (query != null)
                    {
                        query.IdAlumno = alumno.IdAlumno;
                        query.Nombre = alumno.Nombre;
                        query.Genero = (bool)query.Genero;
                        query.Edad = (int)alumno.Edad;
                        
                        context.SaveChanges();
                    }
                    result.Message = "Se modificaron los datos correctamente";
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

        public static ML.Result Delete(int IdAlumno)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {
                    var query = (from Alumno in context.Alumnoes
                                 where Alumno.IdAlumno == IdAlumno
                                 select Alumno).SingleOrDefault();

                    if (query != null)
                    {
                        context.Alumnoes.Remove(query);
                        context.SaveChanges();
                    }
                    result.Message = "Se eliminaron los datos correctamente";
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
