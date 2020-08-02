
using System.Threading.Tasks;
using Microsoft.JSInterop;

public class PlaySound
{
    private readonly IJSRuntime jsRuntime;

    public PlaySound(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public ValueTask Success()
    {
        return this.jsRuntime.InvokeVoidAsync("playSound", "/sounds/success.mp3");
    }

    public ValueTask Buzzer()
    {
        return this.jsRuntime.InvokeVoidAsync("playSound", "/sounds/buzzer.mp3");
    }

}