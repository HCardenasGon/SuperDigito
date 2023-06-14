using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result Calcular(ML.Historial historial)
        {
            ML.Result result = new ML.Result();

            int numero = historial.Numero;

            if (numero >= 9)
            {
                while (numero > 9)
                {
                    string num = numero.ToString();
                    int[] numeros = numero.ToString().Select(o => Convert.ToInt32(o) - 48).ToArray();

                    numero = 0;
                    int x = 0;
                    foreach (char n in numeros)
                    {
                        numero += n;
                        x++;
                    }
                }
            }
            historial.Resultado = numero;

            result.Object = historial;

            result.Correct = true;

            return result;
        }

        public static ML.Result Add(ML.Historial historial)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw($"HistorialAdd {historial.Numero}, {historial.Resultado}, {historial.Usuario.IdUsuario}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Delete(ML.Historial historial)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw($"HistorialDelete {historial.Usuario.IdUsuario}");

                    if(query > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record into the table" + result.Ex;
                //throw;
            }
            return result;
        }

        public static ML.Result Update(ML.Historial historial)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    int queryEF = cnn.Database.ExecuteSqlRaw($"HistorialUpdate {historial.Numero}, '{historial.Usuario.IdUsuario}'");
                    if (queryEF > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record into the Student table" + result.Ex;
                throw;
            }

            return result;
        }

        public static ML.Result GetAll(int idUsuario)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    var query = cnn.Historials.FromSqlRaw($"HistorialGetAllById {idUsuario}").ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var row in query)
                        {
                            ML.Historial historial = new ML.Historial();
                            historial.Numero = row.Numero;
                            historial.Resultado = row.Resultado;
                            historial.FechaHora = row.FechaHora;

                            historial.Usuario = new ML.Usuario();
                            historial.Usuario.IdUsuario = row.IdUsuario;

                            result.Objects.Add(historial);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record into the table" + result.Ex;
                throw;
            }
            return result;
        }

        public static ML.Result findNumero(int idUsuario, int numero)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    var query = cnn.Historials.FromSqlRaw($"HistorialFindNumero {numero}, {idUsuario}").ToList();

                    if (query.Count > 0)
                    {
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = "An error occurred while inserting the record into the table" + result.Ex;
                throw;
            }
            return result;
        }
    }
}
