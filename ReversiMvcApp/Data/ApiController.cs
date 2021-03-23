using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReversiMvcApp.Data
{
    public class ApiController : Controller
    {
        private string baseUrl { get; }

        public ApiController()
        {
            baseUrl = $"http://{Environment.GetEnvironmentVariable("APISource")}/api/";
        }

        private interface IRequest<T>
        {
            public T Empty { get; set; }
            public Task<HttpResponseMessage> Request(HttpClient client, string path);
        }

        private interface IBodyRequest<T> : IRequest<T>
        {
            public T Body { get; set; }
        }

        private class GetRequest<T> : IRequest<T>
        {
            public T Empty { get; set; }

            public async Task<HttpResponseMessage> Request(HttpClient client, string path)
            {
                return await client.GetAsync(path);
            }
        }

        private class PutRequest<T> : IBodyRequest<T>
        {
            public T Empty { get; set; } = default(T);
            public T Body { get; set; }

            public async Task<HttpResponseMessage> Request(HttpClient client, string path)
            {
                return await client.PutAsJsonAsync(path, Body);
            }
        }

        private class PostRequest<T> : IBodyRequest<T>
        {
            public T Empty { get; set; }
            public T Body { get; set; }

            public async Task<HttpResponseMessage> Request(HttpClient client, string path)
            {
                return await client.PostAsJsonAsync(path, Body);
            }
        }

        private async Task<T> MakeRequest<T>(IRequest<T> request, string path)
        {
            using var client = new HttpClient();

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(baseUrl);

            //HTTP GET
            var result = await request.Request(client, path);

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(await result.Content.ReadAsStringAsync());
            }

            return await result.Content.ReadAsAsync<T>();

        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string path)
        {
            return await MakeRequest(new GetRequest<IEnumerable<T>>() {Empty = Enumerable.Empty<T>()}, path);
        }

        public async Task<T> GetAsync<T>(string path)
        {
            return await MakeRequest(new GetRequest<T>() { Empty = default(T) }, path);
        }

        public async Task<T> PutAsync<T>(string path, T data)
        {
            return await MakeRequest(new PutRequest<T>() {Body = data}, path);
        }

        public async Task<T> PostAsync<T>(string path, T data)
        {
            return await MakeRequest(new PostRequest<T>() {Body = data}, path);
        }
    }
}
