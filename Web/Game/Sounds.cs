
using System.Threading.Tasks;
using Microsoft.JSInterop;

public static class Sounds
{
    
    public static Task Success()
    {
        return JSRuntime.Current.InvokeAsync<bool>("playSound", "/sounds/success.mp3");
    }


}