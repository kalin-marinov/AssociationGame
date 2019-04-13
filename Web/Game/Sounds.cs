
using System.Threading.Tasks;
using Microsoft.JSInterop;

public static class PlaySound
{

    public static Task Success()
    {
        return JSRuntime.Current.InvokeAsync<bool>("playSound", "/sounds/success.mp3");
    }

    public static Task Buzzer()
    {
        return JSRuntime.Current.InvokeAsync<bool>("playSound", "/sounds/buzzer.mp3");
    }

}