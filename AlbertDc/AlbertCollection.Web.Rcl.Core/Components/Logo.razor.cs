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
using Microsoft.AspNetCore.Components.Routing;
using AlbertCollection.Application;
using AlbertCollection.Core;

namespace AlbertCollection.Web.Rcl.Core
{
    public partial class Logo
    {
        [Parameter]
        public int HeightInt { get; set; }

        private string SYS_COPYRIGHT_URL = "";
        private string SYS_COPYRIGHT = "";
        private string SYS_DEFAULT_TITLE = "";
        [Inject]
        public IConfigService ConfigService { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            SYS_COPYRIGHT = (await ConfigService.GetByConfigKey(CateGoryConst.Config_SYS_BASE, DevConfigConst.SYS_COPYRIGHT)).ConfigValue;
            SYS_DEFAULT_TITLE = (await ConfigService.GetByConfigKey(CateGoryConst.Config_SYS_BASE, DevConfigConst.SYS_DEFAULT_TITLE)).ConfigValue;
            SYS_COPYRIGHT_URL = (await ConfigService.GetByConfigKey(CateGoryConst.Config_SYS_BASE, DevConfigConst.SYS_COPYRIGHT_URL)).ConfigValue;
            await base.OnParametersSetAsync();
        }
    }
}