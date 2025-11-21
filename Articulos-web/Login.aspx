<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Articulos_web.Login" %>
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
        <h1>Iniciar sesión</h1>
    <div class="col-md-6">
        <div class="mb-3">
            <label class="form-label">Dirección email</label>
            <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" />
            <asp:RegularExpressionValidator ErrorMessage="Ingrese formato email. 'Porejemplo@ejemplo.com'"
             CssClass="validacion" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"   ControlToValidate="txtEmail" runat="server" />
        </div>
        <div class="mb-3">
            <label class="form-label">Contraseña</label>
            <asp:TextBox runat="server" placeholder="*****" ID="txtContraseña" CssClass="form-control" TextMode="Password"/>
        </div>
        <asp:Button Text="Ingresar" ID="btnIngresar" OnClick="btnIngresar_Click" CssClass="btn btn-primary" runat="server" />
        <a href="Default.aspx">Cancelar</a>
    </div>
    </div>

</asp:Content>
