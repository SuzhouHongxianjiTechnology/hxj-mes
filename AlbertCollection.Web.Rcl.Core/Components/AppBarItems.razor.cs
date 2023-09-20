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
using AlbertCollection.Core;

namespace AlbertCollection.Web.Rcl.Core
{
    public partial class AppBarItems
    {
        [Parameter]
        public EventCallback<string> LanguageChange { get; set; }
    }
}