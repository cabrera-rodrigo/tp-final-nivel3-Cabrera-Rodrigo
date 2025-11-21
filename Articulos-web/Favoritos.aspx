<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Articulos_web.Favoritos" EnableEventValidation="false"  %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Favoritos</h1>
 <asp:Repeater ID="rptFavoritos" runat="server">
     <HeaderTemplate>
         <div class="row row-cols-1 row-cols-md-3 g-4">
     </HeaderTemplate>

     <ItemTemplate>
         <div class="col">
             <div class="card">
                 <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="..."
                     onerror="this.src= 'https://png.pngtree.com/png-vector/20221125/ourmid/pngtree-no-image-available-icon-flatvector-illustration-blank-avatar-modern-vector-png-image_40962406.jpg'">
                 <div class="card-body">
                     <h5 class="card-title"><%# Eval("Nombre") %></h5>
                     <p class="card-text"><%# Eval("Descripcion") %></p>
                     <asp:Button Text="Quitar" ID="btnEliminar" OnClick="btnEliminar_Click" CommandName="IdArticulo"
                         CommandArgument='<%#Eval("Id") %>' CssClass="btn btn-danger " runat="server" />
                     <a class="btn btn-primary" href='<%# "Detalle.aspx?Id=" + Eval("Id") %>'>Ver detalle</a>
                 </div>
             </div>
         </div>
     </ItemTemplate>

     <FooterTemplate>
         </div>
     </FooterTemplate>

 </asp:Repeater>
</asp:Content>
