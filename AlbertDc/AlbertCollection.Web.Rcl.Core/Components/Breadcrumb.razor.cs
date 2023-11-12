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
    public partial class Breadcrumb
    {
        [Inject]
        UserResoures UserResoures { get; set; }

        private List<BreadcrumbItem> GetBreadcrumbItems()
        {
            var items = new List<BreadcrumbItem>();
            var currentNav = UserResoures.AllSameLevelMenus.FirstOrDefault(n => n.Component is not null && NavigationManager.Uri.Replace(NavigationManager.BaseUri, "/") == (n.Component));
            if (currentNav is not null)
            {
                if (currentNav.ParentId != 0)
                {
                    var parentNav = UserResoures.AllSameLevelMenus.FirstOrDefault(n => n.Id == currentNav.ParentId);
                    if (parentNav != null)
                        items.Add(new BreadcrumbItem{Text = T(parentNav.Title), Href = null});
                }

                items.Add(new BreadcrumbItem()
                {Text = T(currentNav.Title), Href = currentNav.Component});
                items.Last().Href = currentNav.Component;
            }

            return items;
        }

        private List<BreadcrumbItem> BreadcrumbItems;
        protected override void OnParametersSet()
        {
            BreadcrumbItems = GetBreadcrumbItems();
            base.OnParametersSet();
        }
    }
}