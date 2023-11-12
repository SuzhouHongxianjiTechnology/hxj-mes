namespace AlbertCollection.Web.Rcl.Core
{
    public partial class EnableChip
    {
        [Parameter]
        public string Class { get; set; } = "";
        [Parameter]
        public string Style { get; set; } = "";
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public string DisabledLabel { get; set; }

        [Parameter]
        public string EnabledLabel { get; set; }

        public string TextColor => Value ? "green" : "error";
        public string Color => Value ? "green-lighten" : "warning-lighten";
        string Label => Value ? EnabledLabel ?? T("∆Ù”√") : DisabledLabel ?? T("Õ£”√");
    }
}