using DevExpress.ExpressApp.Blazor.Components.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using DevExpress.ExpressApp.Blazor.Editors;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using XAFBlazorEnumPropertyEditor.Blazor.Server.Components;

namespace XAFBlazorEnumPropertyEditor.Blazor.Server.Editors
{

    [PropertyEditor(typeof(string), "XariEditor", false)]
    public class CustomStringPropertyEditor : BlazorPropertyEditorBase
    {
        public CustomStringPropertyEditor(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        public override InputTextModel ComponentModel => (InputTextModel)base.ComponentModel;
        protected override IComponentModel CreateComponentModel()
        {
            var model = new InputTextModel();
            model.ValueExpression = () => model.Value;
            model.ValueChanged = EventCallback.Factory.Create<string>(this, value => {
                model.Value = value;
                OnControlValueChanged();
                WriteValue();
            });
            return model;
        }
        protected override void ReadValueCore()
        {
            base.ReadValueCore();
            ComponentModel.Value = (string)PropertyValue;
        }
        protected override object GetControlValueCore() => ComponentModel.Value;
        protected override void ApplyReadOnly()
        {
            base.ApplyReadOnly();
            ComponentModel?.SetAttribute("readonly", !AllowEdit);
        }
    }


    public class InputTextModel : ComponentModelBase
    {
        public string Value
        {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public EventCallback<string> ValueChanged
        {
            get => GetPropertyValue<EventCallback<string>>();
            set => SetPropertyValue(value);
        }
        public Expression<Func<string>> ValueExpression
        {
            get => GetPropertyValue<Expression<Func<string>>>();
            set => SetPropertyValue(value);
        }
        public override Type ComponentType => typeof(TextComponent);
    }



}
