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
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;
            ConfirmaEliminacion = false;

            try
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        btnEliminar.Visible = true;
                    }
                    else
                    {
                        btnEliminar.Visible = false;
                    }

                    MarcaDatos marcaDatos = new MarcaDatos();
                    CategoriaDatos categoriaDatos = new CategoriaDatos();
       
                    List<Marca> listaMarcas = marcaDatos.listar();
                    ddlMarca.DataSource = listaMarcas;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();;
                    
                     

                    List<Categoria> listaCategorias = categoriaDatos.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                    if (id != "") 
                    {
                        ArticuloDatos datos = new ArticuloDatos();
                        Articulo seleccionado = (datos.listar(id))[0];
                        txtId.Text = id;
                        txtCodigo.Text = seleccionado.CodigoArticulo;
                        txtNombre.Text = seleccionado.Nombre;
                        txtDescripcion.Text = seleccionado.Descripcion;
                        ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                        ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();
                        txtUrlimagen.Text = seleccionado.ImagenUrl;
                        txtPrecio.Text = seleccionado.Precio.ToString();
                        imgArticulo.ImageUrl = txtUrlimagen.Text;
                    }

                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }

        }


        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;
                Articulo nuevo = new Articulo();
                ArticuloDatos datos = new ArticuloDatos();
                nuevo.CodigoArticulo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                nuevo.ImagenUrl = txtUrlimagen.Text;
                if (string.IsNullOrEmpty(txtPrecio.Text))
                {
                    nuevo.Precio = 0m; 
                }
                else
                    nuevo.Precio = decimal.Parse(txtPrecio.Text);



                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);

                    datos.modificar(nuevo);
                }
                else

                    datos.agregar(nuevo);


                Response.Redirect("Lista Articulos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void txtUrlimagen_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtUrlimagen.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmarEliminacion.Checked) 
                {
                    ArticuloDatos datos = new ArticuloDatos();
                    datos.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("Lista Articulos.aspx", false);

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