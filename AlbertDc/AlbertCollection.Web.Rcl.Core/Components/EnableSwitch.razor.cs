using Masa.Blazor;

namespace AlbertCollection.Web.Rcl.Core
{
    public partial class EnableSwitch
    {
        [Parameter]
        public bool Dense { get; set; } = true;
        [Parameter]
        public string Class { get; set; } = "";
        [Parameter]
        public bool Loading { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        private async Task valueChange(bool value)
        {
            try
            {
                Loading = true;
                StateHasChanged();
                await ValueChanged.InvokeAsync(value);
            }
            catch (Exception ex)
            {
                value = !value;
                await PopupService.EnqueueSnackbarAsync(ex, false);
            }
            finally
            {
                Value = value;
                Loading = false;
            }
        }

        [Parameter]
        public string DisabledLabel { get; set; }

        [Parameter]
        public string EnabledLabel { get; set; }

        string Label => Value ? EnabledLabel ?? T("∆Ù”√") : DisabledLabel ?? T("Õ£”√");
    }
}