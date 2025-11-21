<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="FormularioArticulo.aspx.cs" Inherits="Articulos_web.FormularioArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <style>
        .validacion {
            color: red;
            font-size: 14px;
        }
    </style>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <label for="txtId" class="form-label">Id</label>
                <asp:TextBox runat="server" ID="txtId" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label for="txtCodigo" class="form-label">Codigo de articulo</label>
                <asp:TextBox runat="server" ID="txtCodigo" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="*Campo vacío" CssClass="validacion" ControlToValidate="txtCodigo" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtNombre" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="*Campo vacío" CssClass="validacion" ControlToValidate="txtNombre" runat="server" />
            </div>
            <div class="mb-3">
                <label for="txtDescrìpcion" class="form-label">Descripcion</label>
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripcion" CssClass="form-control" />
                <asp:RequiredFieldValidator ErrorMessage="*Campo vacío" CssClass="validacion" ControlToValidate="txtDescripcion" runat="server" />
            </div>
            <div class="mb-3">
                <label for="ddlMarca" class="form-label">Marca</label>
                <br />
                <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server">
                </asp:DropDownList>
                
            </div>
            <div class="mb-3">
                <label for="ddlCategoria" class="form-label">Categoria</label>
                <br />
                <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" ID="btnAceptar" OnClick="btnAceptar_Click" CssClass="btn btn-primary" runat="server" />
                <a class="btn btn-secondary" href="Lista Articulos.aspx">Cancelar</a>
            </div>
        </div>

        <div class="col-6">
                <div class="mb-3">
                    <label for="txtPrecio" class="form-label">Precio</label>
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" />
                    <asp:RequiredFieldValidator ErrorMessage="*Campo vacío" CssClass="validacion" ControlToValidate="txtPrecio" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="*Solo números" ControlToValidate="txtPrecio"
                     ValidationExpression="^[0-9,]*$" CssClass="validacion"  runat="server" />
                </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="txtUrlimagen" class="form-label">Url imagen</label>
                        <asp:TextBox runat="server" ID="txtUrlimagen" CssClass="form-control"
                            AutoPostBack="true" OnTextChanged="txtUrlimagen_TextChanged" />
                    </div>
                    <asp:Image ImageUrl="https://png.pngtree.com/png-vector/20221125/ourmid/pngtree-no-image-available-icon-flatvector-illustration-blank-avatar-modern-vector-png-image_40962406.jpg"
                        runat="server" ID="imgArticulo" Width="60%"
                        onerror="this.src= 'https://png.pngtree.com/png-vector/20221125/ourmid/pngtree-no-image-available-icon-flatvector-illustration-blank-avatar-modern-vector-png-image_40962406.jpg'"/>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="row">
        <div class="col-6">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                 
                    <div class="mb-3">
                        <asp:Button Text="Eliminar" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-danger" runat="server" />
                    </div>

                    <%if (ConfirmaEliminacion)
                        {%>

                    <div class="mb-3">
                        <asp:CheckBox Text="Confirmar eliminación" ID="chkConfirmarEliminacion" runat="server" />
                        <asp:Button Text="Eliminar" ID="btnConfirmarEliminar" OnClick="btnConfirmarEliminar_Click" CssClass="btn btn-outline-danger" data-bs-dismiss="alert" runat="server" />
                    </div>
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>



    </div>

</asp:Content>
