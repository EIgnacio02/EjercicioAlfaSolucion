using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Beca
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {
                    var query = (from Beca in context.Becas
                                 select new
                                 {
                                     IdBeca = Beca.IdBeca,
                                     Nombre = Beca.Nombre,
                                 });

                    if (query != null && query.ToList().Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Beca beca = new ML.Beca();

                            beca.IdBeca = obj.IdBeca;
                            beca.Nombre = obj.Nombre;

                            result.Objects.Add(beca);
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

        public static ML.Result GetById(int IdBeca)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AlfaSolutionEntities context = new DL.AlfaSolutionEntities())
                {
                    var query = (from Beca in context.Becas
                                 where Beca.IdBeca == IdBeca
                                 select new
                                 {
                                     IdBeca = Beca.IdBeca,
                                     Nombre = Beca.Nombre
                                 }).SingleOrDefault();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        ML.Beca beca = new ML.Beca();

                        beca.IdBeca = query.IdBeca;
                        beca.Nombre = query.Nombre;

                        result.Object = beca;
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
