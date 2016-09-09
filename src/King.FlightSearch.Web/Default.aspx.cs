using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using King.FlightSearch.Services;
using King.FlightSearch.Web.Infrastructure;

namespace King.FlightSearch.Web
{
    public partial class _Default : PageBase
    {
        public FlightService FlightService { get; set; }

        protected string ErrorMessage { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            this.RegisterBindings<FlightSearchModel>()
                .With(p => p.AirportFromId, this.txtDeparture)
                .Save();
        }

        public void OnSearchClick(object sender, EventArgs e)
        {
            var model = this.BindModel<FlightSearchModel>();
            var result = FlightService.Search(model);

            if(!result.IsSuccess)
            {
                this.ErrorMessage = string.Join("<br/>", result.Errors);
                this.divError.Visible = true;
                this.updMessage.Update();
                return;
            }

        }
    }
}