<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="King.FlightSearch.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="updMessage" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="divError" runat="server" visible="false" class="alert alert-danger">
                <%= this.ErrorMessage %>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ChildrenAsTriggers="true" ID="updSearch" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="search-wrapper">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="exampleInputEmail1">Departure airport</label>
                            <asp:TextBox ID="txtDeparture" runat="server" CssClass="form-control" />
                            <asp:HiddenField ID="hdnDepartureId" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 text-right">
                        <asp:LinkButton ID="btnSearch" runat="server" OnClick="OnSearchClick" CssClass="btn btn-success" Text="Search" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
