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
    public partial class AppSheet
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Outlined { get; set; } = true;
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> Attributes { get; set; } = new();
    }
}