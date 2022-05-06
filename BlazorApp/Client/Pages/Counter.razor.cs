using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorApp.Client.Pages
{
    public partial class Counter
    {
        [Inject]
        IJSRuntime JavaScript { get; set; }

        IJSObjectReference module;
        private int currentCount = 0;
        private static int currentCountStatic = 0;

        private async Task IncremenetCountFromJavaScript() 
        {
            await JavaScript.InvokeVoidAsync("dotNetInstanceMethodTest", DotNetObjectReference.Create(this));
        }

        [JSInvokable]
        public async Task IncrementCount()
        {
            module = await JavaScript.InvokeAsync<IJSObjectReference>("import", "./js/Counter.js");
            await module.InvokeVoidAsync("showAlert", "HelloWord");

            currentCount++;
            currentCountStatic = currentCount;

            await JavaScript.InvokeVoidAsync("dotNetStaticMethodTest");
        }

        [JSInvokable]
        public static Task<int> GetCurrentCount() 
        {
            return Task.FromResult(currentCountStatic);
        }
    }
}
