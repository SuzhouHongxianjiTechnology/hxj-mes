using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Masa.Blazor;
using BlazorComponent;
using BlazorComponent.I18n;
using AlbertCollection.Web.Rcl.Core;
using AlbertCollection.Application;

namespace AlbertCollection.Web.Rcl.Core
{
    public partial class ModuleMenu
    {
        private List<DefaultItem> items = new();
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            items = new()
            {new DefaultItem()
            {Heading = I18n.T("Trans")}, };
            items.AddRange(I18n.SupportedCultures.Select(c => new DefaultItem()
            {Title = c.NativeName, Value = c.Name}));
        }

        [Parameter]
        public EventCallback<string> LanguageChange { get; set; }

        private Task OnListItemClick(StringNumber val)
        {
            return LanguageChange.InvokeAsync(val.ToString());
        }
    }
}