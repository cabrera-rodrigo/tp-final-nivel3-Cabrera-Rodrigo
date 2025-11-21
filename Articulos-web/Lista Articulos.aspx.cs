using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Datos;
using Dominio;

namespace Articulos_web
{
    public partial class Lista_Articulos : System.Web.UI.Page
    {
        public bool FiltroAvanzado {  get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Seguridad.esAdmin(Session["registrar"]))
            {
                Session.Add("error", "Se requiere ser admin para acceder a esta pantalla");
                Response.Redirect("Error.aspx");
            }

            if (!IsPostBack)
            {
                FiltroAvanzado = false;
                ArticuloDatos datos = new ArticuloDatos();
                Session.Add("listaArticulos", datos.listar());
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }
        }
        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.DataSource = Session["listaArticulos"];
            dgvArticulos.PageIndex = e.NewPageIndex;
            dgvArticulos.DataBind();
        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            var id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        protected void filtro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(art => art.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            dgvArticulos.DataSource = listaFiltrada;
            dgvArticulos.DataBind();
        }

        protected void chkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;
        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCriterio.Items.Clear();
            if (ddlcampo.SelectedItem.ToString() == "Precio")
            {
                ddlCriterio.Items.Add("Mayor a");
                ddlCriterio.Items.Add("Menor a");
                ddlCriterio.Items.Add("Igual a");
            }
            else 
            {
                ddlCriterio.Items.Add("Comienza con");
                ddlCriterio.Items.Add("Termina con");
                ddlCriterio.Items.Add("Contiene");
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;
                ArticuloDatos datos = new ArticuloDatos();
                dgvArticulos.DataSource = datos.filtrar(ddlcampo.SelectedItem.ToString(),
                ddlCriterio.SelectedItem.ToString(), txtFiltroAvanzado.Text);
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
             ddlcampo.SelectedIndex = -1;
             ddlCriterio.Items.Clear();
             dgvArticulos.DataSource = Session["listaArticulos"];
             dgvArticulos.DataBind();
        }
    }
}