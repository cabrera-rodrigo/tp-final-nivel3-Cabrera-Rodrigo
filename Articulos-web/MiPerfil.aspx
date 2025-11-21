<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Articulos_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 14px;
        }
    </style>
    <div class="row">
        <h1>Mi perfil</h1>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Dirección email</label>
                <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" />
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" placeholder="Nombre" CssClass="form-control" />
                <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="*Ingrese el nombre" ControlToValidate="txtNombre" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="Solo texto..."
                 ValidationExpression="^[a-zA-Z]*$"   ControlToValidate="txtNombre" runat="server" />
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido"  placeholder="Apellido" CssClass="form-control" />
                <asp:RequiredFieldValidator CssClass="validacion" ErrorMessage="*Ingrese el apellido" ControlToValidate="txtApellido" runat="server" />
                <asp:RegularExpressionValidator ErrorMessage="Solo texto..."
                 ValidationExpression="^[a-zA-Z]*$"   ControlToValidate="txtApellido" runat="server" />
            </div>
        </div>
        <div class="col-md-4">
            <div class="mb-3">
                <label class="form-label">Imagen de perfil</label>
                <input type="file" id="txtImagen" runat="server" class="form-control"/>
            </div>

            <asp:Image ID="imgNuevoPerfil" ImageUrl="https://png.pngtree.com/png-vector/20221125/ourmid/pngtree-no-image-available-icon-flatvector-illustration-blank-avatar-modern-vector-png-image_40962406.jpg"
                CssClass="img-fluid mb-3" runat="server" />


        </div>

    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Button Text="Guardar cambios" CssClass="btn btn-primary" ID="btnGuardar" OnClick="btnGuardar_Click" runat="server" />
            <a href="Default.aspx">Regresar</a>
        </div>
    </div>
</asp:Content>
