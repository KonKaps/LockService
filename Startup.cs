using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace TodoApi
{
    public class Startup
    {
        const string url = "https://simonsvoss-homework.herokuapp.com/sv_lsm_data.json";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LockServiceDatabase>(opt =>
               opt.UseInMemoryDatabase("TodoList"));
            services.AddControllers();
        }

public void Configure(IApplicationBuilder app, LockServiceDatabase context, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthorization();

    HttpClient client = new HttpClient();
    //HttpResponseMessage response =  await client.GetAsync(url);
    var responseTask = client.GetAsync(url);
    responseTask.Wait();
    var response = responseTask.Result;
    response.EnsureSuccessStatusCode();

    var responseBodyTask = response.Content.ReadAsStringAsync();
    responseBodyTask.Wait();
    var responseBody = responseBodyTask.Result;

    var jsonData = JsonConvert.DeserializeObject<SecurityData>(responseBody);
    context.WriteData(jsonData);
    context.SaveChanges();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}
    }
}
