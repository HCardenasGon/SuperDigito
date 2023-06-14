using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    int query = cnn.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Username}',  @Password", new SqlParameter("@Password", usuario.Password));

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

        public static ML.Result GetByUsername(string username, byte[] password)
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.SuperDigitoContext cnn = new DL.SuperDigitoContext())
                {
                    var query = cnn.Usuarios.FromSqlRaw($"UsuarioByUsername '{username}',   @Password", new SqlParameter("@Password", password)).ToList().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Username = query.Username;
                        usuario.Password = query.Password;

                        result.Object = usuario;
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
    }
}
