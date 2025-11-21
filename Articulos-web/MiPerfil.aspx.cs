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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sessionActiva(Session["registrar"]))
                    {
                        Registrar usuario = (Registrar)Session["registrar"];
                        txtEmail.Text = usuario.Email;
                        txtEmail.Enabled = false;
                        txtNombre.Text = usuario.Nombre;
                        txtApellido.Text = usuario.Apellido;
                        if (!string.IsNullOrEmpty(usuario.UrlImagenPerfil))
                        {
                            imgNuevoPerfil.ImageUrl = "~/Imagenes/" + usuario.UrlImagenPerfil;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
            
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                RegistrarDatos datos = new RegistrarDatos();
                Registrar usuario = (Registrar)Session["registrar"];
                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                    usuario.UrlImagenPerfil = "perfil-" + usuario.Id + ".jpg";
                }

                    usuario.Nombre = txtNombre.Text;
                    usuario.Apellido = txtApellido.Text;
                
                

                datos.actualizar(usuario);
                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Imagenes/" + usuario.UrlImagenPerfil;
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }
    }
}