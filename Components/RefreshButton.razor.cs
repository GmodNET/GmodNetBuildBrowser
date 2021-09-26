using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace GmodNetBuildBrowser.Components;

public partial class RefreshButton
{
    [EditorRequired]
    [Parameter]
    public Func<Task> OnClick { get; set; }

    [EditorRequired]
    [Parameter]
    public bool IsDisabled { get; set; }

    async Task OnClickHandler()
    {
        if (OnClick is not null)
        {
            await OnClick();
        }
    }
}