using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace Articulos_web
{
    public partial class Mimaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png";

            if (Seguridad.sessionActiva(Session["registrar"]))
            {
                var usuario = Session["registrar"] as Registrar;
                if (usuario != null)
                {
                    lblUsuario.Text = usuario.Email;
                    if (!string.IsNullOrEmpty(usuario.UrlImagenPerfil))
                        imgAvatar.ImageUrl = "~/Imagenes/" + usuario.UrlImagenPerfil;
                }
            }
            else
            {

                if (!(Page is Login || Page is Registro || Page is Default || Page is Error))
                    Response.Redirect("Login.aspx", false);
            }
       
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}