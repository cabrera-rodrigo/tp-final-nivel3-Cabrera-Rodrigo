using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Datos;

namespace Articulos_web
{
    public partial class Favoritos : System.Web.UI.Page
    {
        public List<Articulo> ListaFavoritos { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Seguridad.sessionActiva(Session["registrar"]))
            {
                Session.Add("error", "Debe iniciar sesión...");
                Response.Redirect("Error.aspx");
                //return;
            }

            Registrar user = (Registrar)Session["registrar"];
            string id = Request.QueryString["id"];
            if (user != null && !string.IsNullOrEmpty(id) && int.TryParse(id, out int idArticulo))
            {
                FavoritoDatos negocio = new FavoritoDatos();

                try
                {
                    List<int> favoritosExistentes = negocio.listarFavoritoId(user.Id) ?? new List<int>();

                    if (favoritosExistentes.Contains(idArticulo))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "favExist", "alert('Este artículo ya está en favoritos.');", true);
                    }
                    else
                    {
                        Favorito nuevo = new Favorito
                        {
                            IdUser = user.Id,
                            IdArticulo = idArticulo
                        };
                        negocio.agregarFavorito(nuevo);

                        ScriptManager.RegisterStartupScript(this, GetType(), "favAdded", "alert('Artículo agregado a favoritos.');", true);
                    }
                }
                catch (Exception ex)
                {

                    Session.Add("error", ex.ToString()); 
                    Response.Redirect("Error.aspx");
                    //return;
                }
            }

            ListaFavoritos = new List<Articulo>();

            if (user != null)
            {
                FavoritoDatos negocioart = new FavoritoDatos();
                List<int> idArticulosFavoritos = negocioart.listarFavoritoId(user.Id);
                if (idArticulosFavoritos != null && idArticulosFavoritos.Count > 0)
                {
                    ArticuloDatos art = new ArticuloDatos();
                    ListaFavoritos = art.listarArtId(idArticulosFavoritos);
                    rptFavoritos.DataSource = ListaFavoritos;
                    rptFavoritos.DataBind();
                }
                else
                {
                    rptFavoritos.DataSource = null;
                    rptFavoritos.DataBind();
                }
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            Registrar user = (Registrar)Session["registrar"];
            Button btnEliminar = sender as Button;
            if (btnEliminar == null)
                return;

            if (!int.TryParse(btnEliminar.CommandArgument, out int idArticulo))
            {
                Session["error"] = "Identificador de artículo inválido.";
                Response.Redirect("Error.aspx");
                return;
            }

            if (user != null)
            {
                try
                {
                    FavoritoDatos negocio = new FavoritoDatos();
                    negocio.eliminarFavorito(idArticulo, user.Id);
                }
                catch (Exception ex)
                {
                    Session["error"] = "No se pudo eliminar el favorito. " + ex.Message;
                    Response.Redirect("Error.aspx");
                    return;
                }
                Response.Redirect("Favoritos.aspx");
            }
            else
            {
                Session["error"] = "Sesión inválida al intentar eliminar favorito.";
                Response.Redirect("Error.aspx");
            }
        }
    }
}