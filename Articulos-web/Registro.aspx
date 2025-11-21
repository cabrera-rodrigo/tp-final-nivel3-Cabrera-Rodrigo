<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Articulos_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="col-md-6">

        <h1>Crea tu perfil</h1>
        <div class="mb-3">
            <label class="form-label">Dirección email</label>
            <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" />
            <asp:RegularExpressionValidator ErrorMessage="Solo formato email. 'Porejemplo@ejemplo.com'" 
             ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
                ControlToValidate="txtEmail" runat="server" />
        </div>
        <div class="mb-3">
            <label class="form-label">Contraseña</label>
            <asp:TextBox runat="server" placeholder="*****" ID="txtContraseña" CssClass="form-control" TextMode="Password" />
        </div>
        <asp:Button Text="Registrarse" ID="btnRegistrarse"  CssClass="btn btn-primary" OnClick="btnRegistrarse_Click" runat="server" />
        <a href="Default.aspx">Cancelar</a>
    </div>
    </div>
    
</asp:Content>
