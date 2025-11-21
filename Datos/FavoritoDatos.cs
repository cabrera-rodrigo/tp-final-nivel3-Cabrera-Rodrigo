using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class FavoritoDatos
    {
        public List<int> listarFavoritoId(int idUser) 
        {
            AccesoDatos datos = new AccesoDatos();
            List<int> favoritos = new List<int>();
            try
            {
                datos.setearConsulta("select IdArticulo from FAVORITOS where IdUser = @idUser");
                datos.setearParametro("@idUser", idUser);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    favoritos.Add((int)datos.Lector["IdArticulo"]);
                }
                return favoritos;
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
        public void agregarFavorito(Favorito nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into FAVORITOS (IdUser, IdArticulo) values (@idUser, @idArticulo)");
                datos.setearParametro("@idUser", nuevo.IdUser);
                datos.setearParametro("@idArticulo", nuevo.IdArticulo);
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
        public void eliminarFavorito(int idArticulo, int idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from FAVORITOS where IdUser = @idUser and IdArticulo = @idArticulo");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@idUser", idUser);
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
