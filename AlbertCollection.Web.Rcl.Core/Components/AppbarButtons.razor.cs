namespace AlbertCollection.Web.Rcl.Core
{
    public partial class AppbarButtons
    {
        [Parameter]
        public EventCallback<string> LanguageChange { get; set; }

        //[Parameter]
        //public EventCallback OnSettingsClick { get; set; }
        [Inject]
        UserResoures UserResoures { get; set; }
    }
}