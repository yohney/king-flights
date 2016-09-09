using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace King.FlightSearch.Web.Infrastructure
{
    public class PageBase : Page
    {
        private Dictionary<Type, BindingBuilder> _bindings 
            = new Dictionary<Type, BindingBuilder>();

        public BindingBuilder<TModel> RegisterBindings<TModel>()
            where TModel : class, new()
        {
            return new BindingBuilder<TModel>(this);
        }

        public TModel BindModel<TModel>()
            where TModel : class, new()
        {
            var bindingBuilder = (BindingBuilder<TModel>)this._bindings[typeof(BindingBuilder<TModel>)];

            return bindingBuilder.ToModel();
        }

        public class BindingBuilder
        {
            private PageBase _page;

            public BindingBuilder(PageBase page)
            {
                this._page = page;
            }

            public void Save()
            {
                this._page._bindings[this.GetType()] = this;
            }
        }

        public class BindingBuilder<TModel> : BindingBuilder
            where TModel: class, new()
        {
            public BindingBuilder(PageBase page)
                : base(page)
            {

            }

            public BindingBuilder<TModel> With<TProperty>(Expression<Func<TModel, TProperty>> expression, TextBox txt)
            {
                return this;
            } 

            public TModel ToModel()
            {
                return new TModel();
            }
        }
    }
}