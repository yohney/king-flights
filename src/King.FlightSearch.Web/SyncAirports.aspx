<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SyncAirports.aspx.cs" Inherits="King.FlightSearch.Web.SyncAirports" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="msgSuccess" runat="server" class="alert alert-success">
        <strong>Operation successfull</strong>
    </div>

    <div id="msgError" runat="server" class="alert alert-danger">
        <strong>Operation failed</strong>
        <p>Error: <%= ErrorMessage %></p>
    </div>

</asp:Content>
