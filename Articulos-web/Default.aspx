<%@ Page Title="" Language="C#" MasterPageFile="~/Mimaster.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Articulos_web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />

    <h1>Bienvenido a nuestro catálogo web</h1>
    <p>Explora nuestra colección de artículos interesantes y exclusivos.</p>

    <div class="container">
        <div class="row">
            <aside class="col-12 col-md-3 mb-4">
                <div class="card">
                    <div class="card-body">
                        <h5 class="card-title">Filtros</h5>

                        <div class="mb-3">
                            <label class="form-label">Busqueda rapida</label>
                            <asp:TextBox CssClass="form-control" ID="txtFiltro" placeholder="Buscar por nombre o descripción" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" runat="server" />
                        </div>
                        <div class="mb-3">
                            <label class="form-check-label">Filtro avanzado</label>
                            <asp:CheckBox Text="" CssClass="align-middle" AutoPostBack="true" ID="chkAvanzado" OnCheckedChanged="chkAvanzado_CheckedChanged" runat="server" />

                            <hr />
                        </div>
                        <%if (FiltroAvanzado)
                            {  %>

                        <div class="mb-3">
                            <label class="form-label">Categoria</label>
                            <asp:DropDownList ID="ddlCategoria" CssClass="form-control" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div class="mb-3">
                            <label class="form-label">Marca</label>
                            <asp:DropDownList ID="ddlMarca" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" runat="server">
                            </asp:DropDownList>
                        </div>


                        <div class="d-flex gap-2">
                            <asp:Button CssClass="btn btn-outline-secondary flex-fill" ID="btnLimpiar" OnClick="btnLimpiar_Click" Text="Borrar" runat="server" />
                        </div>
                        <% } %>
                    </div>
                </div>
            </aside>

            <section class="col-12 col-md-9">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <div class="row row-cols-1 row-cols-md-3 g-4">
                            <asp:Repeater ID="rptArticulos" runat="server">
                                <HeaderTemplate>

                                </HeaderTemplate>

                                <ItemTemplate>
                                    <div class="col">
                                        <div class="card">
                                            <img src='<%# Eval("ImagenUrl") %>' class="card-img-top" alt="..."
                                            onerror="this.src= 'https://png.pngtree.com/png-vector/20221125/ourmid/pngtree-no-image-available-icon-flatvector-illustration-blank-avatar-modern-vector-png-image_40962406.jpg'"
                                                >
                                                
                                            <div class="card-body">
                                                <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                                <p class="card-text"><%# Eval("Descripcion") %></p>
                                                <p class="card-text"><%# Eval("Precio", "{0:F2}") %></p>
                                                <a class="btn btn-outline-primary" href='<%# "Favoritos.aspx?Id=" + Eval("Id") %>'>Favorito</a>
                                                <a class="btn btn-primary" href='<%# "Detalle.aspx?Id=" + Eval("Id") %>'>Ver detalle</a>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </div>
                                </FooterTemplate>

                            </asp:Repeater>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </section>
        </div>
    </div>



</asp:Content>
