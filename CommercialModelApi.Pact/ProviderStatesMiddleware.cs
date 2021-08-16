using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CommercialModelApi.Pact
{
    public class ProviderStatesMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Dictionary<string, Action> _states = new Dictionary<string, Action> {
                { "The account 'test' does not exist", () => { File.Delete("/home/wsimons/CommercialModel/test"); } }
            };

        public ProviderStatesMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value == "/provider-states")
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                if (context.Request.Method.ToUpper() == HttpMethod.Post.ToString().ToUpper() &&
                    context.Request.Body != null)
                {
                    string jsonRequestBody = String.Empty;
                    using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8))
                    {
                        jsonRequestBody = await reader.ReadToEndAsync();
                    }
                    var providerState = JsonConvert.DeserializeObject<ProviderState>(jsonRequestBody);

                    //A null or empty provider state key must be handled
                    if (providerState != null && !String.IsNullOrEmpty(providerState.State) &&
                        providerState.Consumer == "Commercial Model CLI")
                    {
                        _states[providerState.State].Invoke();
                    }
                    // await this.HandleProviderStatesRequestAsync(context);
                    await context.Response.WriteAsync(string.Empty);
                }
                else
                {
                    await this._next(context);
                }
            }
        }
    }
}