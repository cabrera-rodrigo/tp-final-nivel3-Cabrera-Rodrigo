using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;


namespace Articulos_web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Registrar registrar = new Registrar();
            RegistrarDatos datos = new RegistrarDatos();

            if (Validacion.validarTextoVacio(txtEmail) || Validacion.validarTextoVacio(txtContraseña))
            {
                Session.Add("error", "Debes completar los campos vacios...");
                Response.Redirect("Error.aspx");
            }
            try
            {
                registrar.Email = txtEmail.Text;
                registrar.Contraseña = txtContraseña.Text;
                if (datos.Login(registrar))
                {
                    Session.Add("registrar", registrar);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session.Add("error", "Email o contraseña incorrectos");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
            
        }
    }
}