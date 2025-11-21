using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace Datos
{
    public class ArticuloDatos
    {
        public List<Articulo> listar(string id="")
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = ("SELECT Codigo, Nombre, A.Descripcion, ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, A.Precio, A.IdMarca, A.IdCategoria, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M WHERE C.Id = A.IdCategoria AND A.IdMarca = M.Id ");
                if(id != "")
                    consulta += " AND A.Id = " + id;
                datos.setearConsulta(consulta); 
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.descripcion = (string)datos.Lector["Categoria"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    lista.Add(aux);
                }

                return lista;

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
        public List<Articulo> listarArtId(List<int> listaArtId)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, Codigo, Nombre, A.Descripcion, M.Descripcion TipoMarca, " +
                    "C.Descripcion TipoCategoria, ImagenUrl, A.Precio, A.IdMarca, A.IdCategoria " +
                    "FROM ARTICULOS A, CATEGORIAS C, MARCAS M " +
                    "WHERE C.Id = A.IdCategoria AND A.IdCategoria = M.Id AND A.Id IN (" + string.Join(",", listaArtId) + ")";
                datos.setearConsulta(consulta);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.descripcion = (string)datos.Lector["TipoMarca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.descripcion = (string)datos.Lector["TipoCategoria"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

            public List<Articulo> listarConSP()
            {
                List<Articulo> lista = new List<Articulo>();
                AccesoDatos datos = new AccesoDatos();
                try
                {

                    datos.setearProcedimiento("stListar");
                    datos.ejecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Articulo aux = new Articulo();
                        aux.Id = (int)datos.Lector["Id"];
                        aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                        aux.Nombre = (string)datos.Lector["Nombre"];
                        aux.Descripcion = (string)datos.Lector["Descripcion"];
                        if (!(datos.Lector["ImagenUrl"] is DBNull))
                            aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                        aux.Marca = new Marca();
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];
                        aux.Marca.descripcion = (string)datos.Lector["Marca"];
                        aux.Categoria = new Categoria();
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                        aux.Categoria.descripcion = (string)datos.Lector["Categoria"];
                        aux.Precio = (decimal)datos.Lector["Precio"];
                        lista.Add(aux);
                    }

                    return lista;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        public void agregar(Articulo nuevo)
        {
            
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConsulta("insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)values('" + nuevo.CodigoArticulo + "', '" + nuevo.Nombre + "', '" + nuevo.Descripcion + "' ,@IdMarca , @IdCategoria , '" + nuevo.ImagenUrl + "', " + nuevo.Precio + ")");
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
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

        public void agregarConSP(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedAltaArticulo");
                datos.setearParametro("@codigo", nuevo.CodigoArticulo);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@desc", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@imagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@precio", nuevo.Precio);
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

        public void modificarConSP(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedModificarArticulo");
                datos.setearParametro("@id", modificar.Id);
                datos.setearParametro("@codigo", modificar.CodigoArticulo);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@desc", modificar.Descripcion);
                datos.setearParametro("@IdMarca", modificar.Marca.Id);
                datos.setearParametro("@IdCategoria", modificar.Categoria.Id);
                datos.setearParametro("@imagenUrl", modificar.ImagenUrl);
                datos.setearParametro("@precio", modificar.Precio);
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
        public void modificar(Articulo modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.setearParametro("@Codigo", modificar.CodigoArticulo);
                datos.setearParametro("@Nombre", modificar.Nombre);
                datos.setearParametro("@Descripcion", modificar.Descripcion);
                datos.setearParametro("@IdMarca", modificar.Marca.Id);
                datos.setearParametro("@IdCategoria", modificar.Categoria.Id);
                datos.setearParametro("@ImagenUrl", modificar.ImagenUrl);
                datos.setearParametro("@Precio", modificar.Precio);
                datos.setearParametro("@Id", modificar.Id);
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
        public void eliminarConSP(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedEliminarArticulo");
                datos.setearParametro("@id", id);
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
        public void eliminar(int id) 
        {
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @Id");
                datos.setearParametro("@Id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT Codigo, Nombre, A.Descripcion, ImagenUrl, M.Descripcion Marca, C.Descripcion Categoria, A.Precio, A.IdMarca, A.IdCategoria, A.Id from ARTICULOS A, CATEGORIAS C, MARCAS M WHERE C.Id = A.IdCategoria AND A.IdMarca = M.Id AND ";
                switch (campo)
                {
                    case "Nombre":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "Nombre LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "Nombre LIKE '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "Nombre LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Descripcion":
                            switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.Descripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "A.Descripcion LIKE '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "A.Descripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "M.Descripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "M.Descripcion LIKE '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "M.Descripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Categoria":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "C.Descripcion LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "C.Descripcion LIKE '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "C.Descripcion LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Codigo":
                        switch (criterio) 
                        {
                            case "Comienza con":
                                consulta += "Codigo LIKE '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "Codigo LIKE '%" + filtro + "'";
                                break;
                            case "Contiene":
                                consulta += "Codigo LIKE '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.Precio > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "A.Precio < " + filtro;
                                break;
                            case "Igual a":
                                consulta += "A.Precio = " + filtro;
                                break;
                        }
                        break;

                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.descripcion = (string)datos.Lector["Categoria"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
            public List<Articulo> filtrarDDL(string categoria, string marca)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = @"SELECT Codigo, Nombre, A.Descripcion, ImagenUrl,
                            M.Descripcion Marca, C.Descripcion Categoria,
                            A.Precio, A.IdMarca, A.IdCategoria, A.Id
                            FROM ARTICULOS A
                            INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria
                            INNER JOIN MARCAS M ON M.Id = A.IdMarca
                            WHERE 1=1";

                if (categoria != "0")
                    consulta += " AND C.Id = " + categoria;

                if (marca != "0")
                    consulta += " AND M.Id = " + marca;

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.CodigoArticulo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.ImagenUrl = datos.Lector["ImagenUrl"] is DBNull ? null : (string)datos.Lector["ImagenUrl"];

                    aux.Marca = new Marca
                    {
                        Id = (int)datos.Lector["IdMarca"],
                        descripcion = (string)datos.Lector["Marca"]
                    };

                    aux.Categoria = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        descripcion = (string)datos.Lector["Categoria"]
                    };

                    aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
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

