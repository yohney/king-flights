using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using King.FlightSearch.DAL.Shared;

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
                var propertyBinder = new PropertyBinder<TModel, TProperty>();
                propertyBinder.PropertySelector = expression;
                propertyBinder.ValueExtractor = this.GetParsedValue<TProperty>(() => txt.Text);
                this._properties.Add(propertyBinder);

                return this;
            }

            public BindingBuilder<TModel> With<TProperty>(Expression<Func<TModel, TProperty>> expression, DropDownList ddl)
            {
                var propertyBinder = new PropertyBinder<TModel, TProperty>();
                propertyBinder.PropertySelector = expression;
                propertyBinder.ValueExtractor = this.GetParsedValue<TProperty>(() => ddl.SelectedValue);
                this._properties.Add(propertyBinder);

                return this;
            }

            public BindingBuilder<TModel> With<TProperty>(Expression<Func<TModel, TProperty>> expression, HiddenField hdn)
            {
                var propertyBinder = new PropertyBinder<TModel, TProperty>();
                propertyBinder.PropertySelector = expression;
                propertyBinder.ValueExtractor = this.GetParsedValue<TProperty>(() => hdn.Value);
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

            private Func<TProperty> GetParsedValue<TProperty>(Func<string> valueSelector)
            {
                var destinationType = typeof(TProperty);
                var isNullable = false;

                if(destinationType.IsNullable())
                {
                    isNullable = true;
                    destinationType = destinationType.GetUnderlyingTypeForNullable();
                }

                if (destinationType.IsEnum)
                    return () => {
                        var val = valueSelector();
                        int outVal;
                        if (int.TryParse(val, out outVal))
                            return (TProperty)(object)outVal;

                        return (TProperty)(object)Enum.Parse(destinationType, valueSelector());
                    };

                if (destinationType == typeof(Guid))
                    return () => (TProperty)(object)Guid.Parse(valueSelector());

                if (destinationType == typeof(string))
                    return () => (TProperty)(object)valueSelector();

                if (destinationType == typeof(DateTime))
                    return () =>
                    {
                        if (isNullable && string.IsNullOrWhiteSpace(valueSelector()))
                            return (TProperty)(object)null;

                        return (TProperty)(object)DateTime.ParseExact(valueSelector(), "d.M.yyyy", null);
                    };

                if (destinationType == typeof(int))
                    return () => (TProperty)(object)int.Parse(valueSelector());

                throw new ArgumentException();
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