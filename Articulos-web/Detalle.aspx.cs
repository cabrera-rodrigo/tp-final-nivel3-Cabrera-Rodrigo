using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace Articulos_web
{
    public partial class Detalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
			try
			{
				if (!IsPostBack)
				{
					MarcaDatos marcaDatos = new MarcaDatos();
					ddlMarca.DataSource = marcaDatos.listar();
					ddlMarca.DataValueField = "Id";
					ddlMarca.DataTextField = "Descripcion";
					ddlMarca.DataBind();

					CategoriaDatos categoriaDatos = new CategoriaDatos();
					ddlCategoria.DataSource = categoriaDatos.listar();
					ddlCategoria.DataValueField = "Id";
					ddlCategoria.DataTextField = "Descripcion";
					ddlCategoria.DataBind();
                }
              
                
                string id = Request.QueryString["Id"];
                if (!string.IsNullOrWhiteSpace(id))
                {
                    ArticuloDatos datos = new ArticuloDatos();
                    var articulo = datos.listar(id).FirstOrDefault();
                    if (articulo != null)
                    {
                        txtId.Text = articulo.Id.ToString();
                        txtId.Enabled = false;
                        txtCodigo.Text = articulo.CodigoArticulo;
                        txtCodigo.Enabled = false;
                        txtNombre.Text = articulo.Nombre;
                        txtNombre.Enabled = false;
                        txtDescripcion.Text = articulo.Descripcion;
                        txtDescripcion.Enabled = false;
                        ddlMarca.SelectedValue = articulo.Marca.Id.ToString();
                        ddlMarca.Enabled = false;
                        ddlCategoria.SelectedValue = articulo.Categoria.Id.ToString();
                        ddlCategoria.Enabled = false;
                        txtPrecio.Text = articulo.Precio.ToString("F2");
                        txtPrecio.Enabled = false;
                        txtUrlImagen.Text = articulo.ImagenUrl;
                        imgArticulo.ImageUrl = articulo.ImagenUrl;
                        txtUrlImagen.Enabled = false;

                    }
                }
            }
			catch (Exception ex)
			{

				Session.Add("Error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }
    }
}