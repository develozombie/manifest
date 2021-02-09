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
using System.Text.Json.Serialization;


namespace joyapu.Function
{
    public static class GenerateManifest
    {
        
        [FunctionName("GenerateManifest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            //Crea lista de Json Item para guardar elementos con Guid Generados
            List<JsonItem> listItems = new List<JsonItem>();
            //Obtiene datos del request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //Deserializa el Body en una Lista de JsonItem
            dynamic jsonItems = JsonSerializer.Deserialize<List<JsonItem>>(requestBody);
            foreach (var item in jsonItems)
            {
                item.RoleGuid = Guid.NewGuid().ToString();
                listItems.Add(item);
            }
            //req.HttpContext.Response.Headers.Add("Content-Type", "application/json");
            string jsonBody = JsonSerializer.Serialize(listItems);
            string responseMessage = string.IsNullOrEmpty(jsonBody)
                ? "Error"
                : jsonBody;
            return new OkObjectResult(responseMessage);
        }
    }

    public class JsonItem
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public string RoleGuid { get; set; }
    }
}
