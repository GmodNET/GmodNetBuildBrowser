using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GmodNetBuildBrowser.Components;

public partial class RefreshButton
{
    [EditorRequired]
    [Parameter]
    public Func<Task> OnClick { get; set; }

    bool isDisabled = false;

    async Task OnClickHandler()
    {
        isDisabled = true;

        if (OnClick is not null)
        {
            await OnClick();
        }

        isDisabled = false;
    }
}