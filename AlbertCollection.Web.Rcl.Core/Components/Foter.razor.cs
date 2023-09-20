namespace AlbertCollection.Web.Rcl.Core
{
    public partial class Foter
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