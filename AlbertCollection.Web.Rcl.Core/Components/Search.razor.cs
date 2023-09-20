namespace AlbertCollection.Web.Rcl.Core
{
    public partial class Search
    {
        private string _value;
        private string Value
        {
            get => _value;
            set
            {
                _value = value;
                if (!string.IsNullOrEmpty(value))
                {
                    NavigationManager.NavigateTo(value);
                }
            }
        }

        [Inject]
        [NotNull]
        private NavigationManager NavigationManager { get; set; }

        private List<SysResourceAc> AvalidMenus;
        [Inject]
        private UserResoures UserResoures { get; set; }

        protected override void OnInitialized()
        {
            AvalidMenus = UserResoures.SameLevelMenus.Where(it => it.Component != null).ToList();
            base.OnInitialized();
        }
    }
}