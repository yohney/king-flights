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
            private List<PropertyBinder<TModel>> _properties;

            public BindingBuilder(PageBase page)
                : base(page)
            {
                this._properties = new List<Infrastructure.PageBase.PropertyBinder<TModel>>();
            }

            public BindingBuilder<TModel> With<TProperty>(Expression<Func<TModel, TProperty>> expression, TextBox txt)
            {
                return this;
            }

            public BindingBuilder<TModel> With<TProperty>(Expression<Func<TModel, TProperty>> expression, HiddenField hdn)
            {
                var propertyBinder = new PropertyBinder<TModel, TProperty>();
                propertyBinder.PropertySelector = expression;

                if(typeof(TProperty) == typeof(Guid))
                    propertyBinder.ValueExtractor = () => (TProperty)(object)Guid.Parse(hdn.Value);
                
                if(typeof(TProperty) == typeof(string))
                    propertyBinder.ValueExtractor = () => (TProperty)(object)hdn.Value;

                this._properties.Add(propertyBinder);

                return this;
            }

            public TModel ToModel()
            {
                var model = new TModel();

                foreach (var prop in this._properties)
                    prop.SetModelProperty(model);

                return model;
            }
        }

        public abstract class PropertyBinder<TModel>
        {
            public abstract void SetModelProperty(TModel model);
        }

        public class PropertyBinder<TModel, TProperty> : PropertyBinder<TModel>
        {
            public Expression<Func<TModel, TProperty>> PropertySelector { get; set; }
            public Func<TProperty> ValueExtractor { get; set; }

            public override void SetModelProperty(TModel model)
            {
                model.SetPropertyValue(PropertySelector, ValueExtractor());
            }
        }
    }
}