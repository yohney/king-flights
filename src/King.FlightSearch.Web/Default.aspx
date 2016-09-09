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
                            <label for="txtDeparture">Departure airport</label>
                            <asp:TextBox ID="txtDeparture" runat="server" CssClass="form-control" placeholder="Type airport name" />
                            <asp:HiddenField ID="hdnDepartureId" runat="server" />
                            <asp:RequiredFieldValidator ID="vldDeparture" runat="server" Text="Departure airport is obligatory!" ControlToValidate="txtDeparture" ForeColor="Red" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtDestination">Destination airport</label>
                            <asp:TextBox ID="txtDestination" runat="server" CssClass="form-control" placeholder="Type airport name" />
                            <asp:HiddenField ID="hdnDestinationId" runat="server" />
                            <asp:RequiredFieldValidator ID="vldDestination" runat="server" Text="Destination airport is obligatory!" ControlToValidate="txtDestination" ForeColor="Red" />
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

    <script type="text/javascript">
        $(document).ready(function () {
            registerAutocomplete(
                $("#<%= txtDeparture.ClientID %>"),
                $("#<%= hdnDepartureId.ClientID %>"),
                "/api/airports/suggest/");

            registerAutocomplete(
                $("#<%= txtDestination.ClientID %>"),
                $("#<%= hdnDestinationId.ClientID %>"),
                "/api/airports/suggest/");
        });

        function registerAutocomplete(txt, hdn, url) {
            txt.autocomplete({
                minLength: 2,
                focus: function (event, ui) {
                    txt.val(ui.item.label);
                    return false;
                },
                select: function (event, ui) {
                    txt.val(ui.item.label);
                    hdn.val(ui.item.value);
                    return false;
                },
                source: function (req, resp) {
                    $.getJSON(url + "/" + req.term, {}, resp);
                }
            }).click(function () {
                $(this).select();
            });
        }
    </script>

</asp:Content>
