

using System;
using System.Threading;
using System.Threading.Tasks;

static class TurnTimer
{
    static CancellationTokenSource cts;

    public static async Task Wait(TimeSpan waitTime)
    {
        cts = new CancellationTokenSource();

        try
        {
            await Task.Delay(waitTime, cts.Token);
        }
        catch (TaskCanceledException)
        {
        }

        cts = null;
    }

    public static void Cancel()
    {
        cts?.Cancel();
    }

}