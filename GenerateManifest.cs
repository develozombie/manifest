using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;


namespace joyapu.Function
{
    public static class GenerateManifest
    {
        
        [FunctionName("GenerateManifest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            //Crea lista de AppRoles para guardar elementos con Guid Generados
            List<AppRoles> listItems = new List<AppRoles>();
            //Obtiene datos del request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Deserializa el Body en una Lista de JsonItem
            try
            {
                dynamic jsonItems = JsonSerializer.Deserialize<List<JsonItem>>(requestBody);
                foreach (var item in jsonItems)
                {
                    listItems.Add(
                        new AppRoles{
                            allowedMemberTypes = new string[]{"User"},
                            displayName = item.RoleName,
                            id = Guid.NewGuid().ToString(),
                            isEnabled = true,
                            description = item.RoleDescription,
                            value = item.RoleName.Replace(" ", "_").ToLower()
                    });
                }
                //req.HttpContext.Response.Headers.Add("Content-Type", "application/json");
                string jsonBody = JsonSerializer.Serialize(listItems);
                return new OkObjectResult(jsonBody);
            }
            catch (System.Exception)
            {

                return new BadRequestObjectResult("Error: Revise la estructura JSON");
                throw;
            }
        }
    }

    public class JsonItem
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }

    public class AppRoles
    {
        public string[] allowedMemberTypes { get; set; }
        public string displayName { get; set; }
        public string id { get; set; }
        public bool isEnabled { get; set; }
        public string description { get; set; }
        public string value { get; set; }
    }
}
