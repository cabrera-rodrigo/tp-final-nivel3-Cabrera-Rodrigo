using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using System.Web.Services.Description;

namespace Articulos_web
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaDatos categoriaDatos = new CategoriaDatos();
            MarcaDatos marcaDatos = new MarcaDatos();
            ArticuloDatos datos = new ArticuloDatos();
            try
            {
                    FiltroAvanzado = chkAvanzado.Checked;
                    Session["listaArticulos"] = datos.listar() ?? new List<Articulo>();
                    ListaArticulo = datos.listar() ?? new List<Articulo>();
                if (!IsPostBack) 
                {
 
                    List<Articulo> listaArticulos = datos.listar();
                    Session["listaArticulos"] = listaArticulos;

                    List<Categoria> listaCategorias = categoriaDatos.listar();
                    ddlCategoria.DataSource = listaCategorias;
                    ddlCategoria.DataTextField = "descripcion";
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataBind();

                    rptArticulos.DataSource = ListaArticulo;
                    rptArticulos.DataBind();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }


            

        }
        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(ddlCategoria.SelectedValue);


            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];


            var marcasFiltradas = lista
                .Where(a => a.Categoria.Id == idCategoria)
                .Select(a => a.Marca)
                .GroupBy(m => m.Id)
                .Select(g => g.First())
                .ToList();

 
            ddlMarca.Items.Clear();

            ddlMarca.DataSource = marcasFiltradas;
            ddlMarca.DataTextField = "descripcion";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();


            ddlMarca.Items.Insert(0, new ListItem("-- Todas --", "0"));
            FiltrarArticulosDDL();
            
        }
        private void FiltrarArticulosDDL()
        {
            ArticuloDatos datos = new ArticuloDatos();
            string categoria = ddlCategoria.SelectedValue;
            string marca = ddlMarca.SelectedValue;

            ListaArticulo = datos.filtrarDDL(categoria, marca);
            Session["listaArticulos"] = ListaArticulo;

            rptArticulos.DataSource = ListaArticulo;
            rptArticulos.DataBind();

        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            ListaArticulo = (List<Articulo>)Session["listaArticulos"];
            ListaArticulo = ListaArticulo.FindAll(art => art.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()) || art.Descripcion.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            rptArticulos.DataSource = ListaArticulo;
            rptArticulos.DataBind();

        }
        public bool FiltroAvanzado {  get; set; }
        
        

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            try
            {

                txtFiltro.Enabled = !chkAvanzado.Checked;

                ArticuloDatos datos = new ArticuloDatos();
                Session["listaArticulos"] = datos.listar();
                //ListaArticulo = datos.listar();
                rptArticulos.DataSource = ListaArticulo;
                rptArticulos.DataBind();

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw ex;
            }
        }

        protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulosDDL();
        }
    }
}