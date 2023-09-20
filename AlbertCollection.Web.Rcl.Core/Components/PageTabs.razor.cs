using Masa.Blazor.Presets;

namespace AlbertCollection.Web.Rcl.Core
{
    public partial class PageTabs
    {
        private TabOptions TabOptions(PageTabPathValue value)
        {
            var item = UserResoures.PageTabItems.FirstOrDefault(u => value.IsMatch(u.Href));
            var title = T(item?.Title);
            var icon = item?.Icon;
            var titleClass = $"mx-2 text-capitalize {(value.Selected ? "primary--text" : "")}";
            var op = new TabOptions(title, icon, titleClass);
            op.TitleStyle = "min-width:46px;";
            op.Class = "tgTab";
            return op;
        }

        public PPageTabs PPageTabs { get; private set; }

        [Inject]
        UserResoures UserResoures { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}