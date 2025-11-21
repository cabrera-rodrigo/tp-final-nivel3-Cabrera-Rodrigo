using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Datos
{
    public class RegistrarDatos
    {
        public int registrarNuevo(Registrar nuevo)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearProcedimiento("storedRegistrarNuevo");
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Contraseña);
                return datos.ejecutarAccionScalar();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                datos.cerrarConexion();
            }

        }

        public bool Login(Registrar registrar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select id, email, pass, admin, urlImagenPerfil, nombre, apellido from USERS where email = @email and pass = @pass");
                datos.setearParametro("@email", registrar.Email);
                datos.setearParametro("@pass", registrar.Contraseña);
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    registrar.Id = (int)datos.Lector["id"];
                    registrar.Admin = (bool)datos.Lector["admin"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        registrar.UrlImagenPerfil = (string)datos.Lector["urlImagenPerfil"];
                    if (!(datos.Lector["nombre"] is DBNull))
                        registrar.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        registrar.Apellido = (string)datos.Lector["apellido"];
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                datos.cerrarConexion();
            }
        }

        public void actualizar(Registrar usuario)
        {
                AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update USERS set urlImagenPerfil = @urlimagenperfil, nombre = @nombre, apellido = @apellido where Id = @id");
                datos.setearParametro("@urlimagenperfil", usuario.UrlImagenPerfil ?? (object)DBNull.Value);
                datos.setearParametro("@id", usuario.Id);
                datos.setearParametro("@nombre", usuario.Nombre);
                datos.setearParametro("@apellido", usuario.Apellido);
                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally 
            {
                datos.cerrarConexion();
            }
        }
    }
}
