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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            if (Validacion.validarTextoVacio(txtEmail) || Validacion.validarTextoVacio(txtContraseña))
            {
                Session.Add("error", "Debes completar los campos vacios...");
                Response.Redirect("Error.aspx");
            }
            try
            {

                Registrar usuario = new Registrar();
                RegistrarDatos registrarDatos = new RegistrarDatos();
                EmailService emailService = new EmailService();

                usuario.Email = txtEmail.Text;
                usuario.Contraseña = txtContraseña.Text;
                usuario.Id = registrarDatos.registrarNuevo(usuario);
                Session.Add("registrar", usuario);

                emailService.armarCorreo(usuario.Email, "Registro exitoso", "Hola te damos la bienvenida a nuestra pagina");
                emailService.enviarEmail();
                Response.Redirect("Default.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("error", ex.ToString());
            }
        }
    }
}