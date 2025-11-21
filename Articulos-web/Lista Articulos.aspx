<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="Lista Articulos.aspx.cs" Inherits="Articulos_web.Lista_Articulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
        <style>
        .validacion {
            color: red;
            font-size: 14px;
        }
    </style>
    <h1>Listado de articulos</h1>
        <asp:UpdatePanel runat="server">
        <ContentTemplate>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
             <asp:Label Text="Filtrar" runat="server" />
             <asp:TextBox ID="txtFiltro" AutoPostBack="true" placeholder="Buscar por nombre" CssClass="form-control" OnTextChanged="filtro_TextChanged" runat="server" />
            </div>
        </div>
        <div class="col-6" style="display: flex; flex-direction:column; justify-content:flex-end;">
            <div class="mb-3">
                <asp:CheckBox Text="Filtro Avanzado" 
                 CssClass="" ID="chkAvanzado"
                 AutoPostBack="true"
                 OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="mb-3">
                <asp:Button Text="Limpiar filtro" CssClass="btn btn-primary" ID="btnLimpiar" OnClick="btnLimpiar_Click" runat="server" />
            </div>
        </div>
        <%if (chkAvanzado.Checked)
            { %>
        <script>    

            function ValidarPrecio() {
                var ddlCampo = document.getElementById('<%= ddlcampo.ClientID %>');
                var ddlCriterio = document.getElementById('<%= ddlCriterio.ClientID %>');
                var txtFiltroAvanzado = document.getElementById('<%= txtFiltroAvanzado.ClientID %>');
                if (ddlCampo.value === "Precio") {
                    var filtroValue = parseFloat(txtFiltroAvanzado.value);
                    if (isNaN(filtroValue)) {
                        alert("Por favor, ingrese un valor numérico válido para el precio.");
                        return false;
                    }
                }
                return true;
            }
        </script>

        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Campo" ID="txtCampo" runat="server" />
                    <asp:DropDownList runat="server" ID="ddlcampo" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCampo_SelectedIndexChanged">    
                        <asp:ListItem Text="" />
                        <asp:ListItem Text="Codigo" />
                        <asp:ListItem Text="Nombre" />
                        <asp:ListItem Text="Precio" />
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="*Seleccione un campo..." CssClass="validacion" ControlToValidate="ddlcampo" runat="server" />
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Criterio" runat="server" />
                    <asp:DropDownList ID="ddlCriterio" CssClass="form-control" runat="server"></asp:DropDownList>
                    <asp:RequiredFieldValidator ErrorMessage="*Seleccione un criterio.." CssClass="validacion" ControlToValidate="ddlCriterio" runat="server" />
                </div>
            </div>
            <div class="col-3">
                <div class="mb-3">
                    <asp:Label Text="Filtro" runat="server" />
                    <asp:TextBox ID="txtFiltroAvanzado" CssClass="form-control" runat="server" />
                </div>
            </div>

        </div>
        <div class="row">
            <div class="col-3">
                <div class="mb-3">
                    <asp:Button Text="Buscar" OnClientClick="return ValidarPrecio()" cssclass="btn btn-primary" ID="btnBuscar" OnClick="btnBuscar_Click" runat="server" />
                </div>
            </div>
        </div>
       <% } %>
    </div>
   

    <asp:GridView ID="dgvArticulos" runat="server" DataKeyNames="Id"
        OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
        CssClass="table" AutoGenerateColumns="false"
        OnPageIndexChanging="dgvArticulos_PageIndexChanging"
        AllowPaging="True" PageSize="4">


        <Columns>
            <asp:BoundField DataField="CodigoArticulo" HeaderText="Codigo" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:F2}" />  
            <asp:CommandField ShowSelectButton="true" SelectText="Modificar" HeaderText="Accion" />
        </Columns>


    </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
    <a class="btn btn-primary" href="FormularioArticulo.aspx">Agregar</a>
</asp:Content>
