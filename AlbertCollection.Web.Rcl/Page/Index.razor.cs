using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using AlbertCollection.Application;
using AlbertCollection.Core;
using System.Linq.Expressions;
using System.Globalization;
using BlazorComponent;
using NewLife.Serialization;
using AlbertCollection.Web.Rcl.Core;
using SqlSugar;
using Microsoft.Extensions.Options;
using AlbertCollection.Web.Rcl;
using System.ComponentModel;
using Masa.Blazor;
using AlbertCollection.Core.Extension;
using Masa.Blazor.Presets;

namespace AlbertCollection.Web.Rcl
{
    public partial class Index
    {
        [Inject]
        private IVisitLogService VisitLogService { get; set; }

        List<DevLogVisit> DevLogVisits;
        [Inject]
        private IOperateLogService OperateLogService { get; set; }

        List<DevLogOperate> DevLogOps;
        protected override async Task OnInitializedAsync()
        {
            DevLogVisits = (await VisitLogService.Page(new()
            {Size = 5, Account = UserResoures.CurrentUser?.Account})).Records.ToList();
            DevLogOps = (await OperateLogService.Page(new()
            {Size = 5, Account = UserResoures.CurrentUser?.Account})).Records.ToList();
            await base.OnInitializedAsync();
        }
    }
}