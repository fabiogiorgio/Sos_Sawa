using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ExtraFunction.Model;
using System;
using System.Net;
using System.Threading.Tasks;
using ExtraFunction.Repository_.Interface;
using ExtraFunction.Authorization;
using ExtraFunction.Service;
using ExtraFunction.DTO_;
using Newtonsoft.Json;
using System.IO;

namespace ExtraFunction.Control
{
    public class DisclaimersController
    {
        private readonly ILogger<DisclaimersController> _logger;
        private readonly IDisclaimersService _disclaimersService;

        public DisclaimersController(ILogger<DisclaimersController> log, IDisclaimersService disclaimersService)
        {
            _logger = log;
            _disclaimersService = disclaimersService;   
        }

        [Function(nameof(GetDisclaimers))]
        [OpenApiOperation(operationId: "GetDisclaimers", tags: new[] { "Disclaimers" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Disclaimers), Description = "Successfully received disclaimers")]
        public async Task<HttpResponseData> GetDisclaimers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Disclaimers")] HttpRequestData req) //route is emtpy
        {

            HttpResponseData responseData = req.CreateResponse();

            try
            {
                Disclaimers disclaimer = await _disclaimersService.GetDisclaimers();
                responseData = req.CreateResponse(HttpStatusCode.OK);

                await responseData.WriteAsJsonAsync(disclaimer);
                return responseData;
            }
            catch (Exception e)
            {

                responseData.StatusCode = HttpStatusCode.BadRequest;
                responseData.Headers.Add("Reason", e.Message);
            }

            return responseData;
        }

        [Function(nameof(UpdateDisclaimers))]
        [OpenApiOperation(operationId: "Updates Disclaimer", tags: new[] { "CMS" }, Summary = "Updates a disclaimer", Description = "This endpoint update a disclaimer")]
        [OpenApiRequestBody("application/json", bodyType: typeof(UpdateDisclaimersDTO), Description = "The disclaimer data.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(UpdateDisclaimersDTO), Description = "The OK response with the updated disclaimer.")]
        public async Task<HttpResponseData> UpdateDisclaimers([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req)
        {
            _logger.LogInformation("Updating Disclaimers");

            HttpResponseData responseData = req.CreateResponse();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateDisclaimersDTO updateDisclaimerDTO = JsonConvert.DeserializeObject<UpdateDisclaimersDTO>(requestBody);
            try
            {
                await _disclaimersService.UpdateDisclaimers(updateDisclaimerDTO);
                responseData.StatusCode = HttpStatusCode.OK;
                return responseData;
            }
            catch (Exception e)
            {
                responseData.StatusCode = HttpStatusCode.BadRequest;
                responseData.Headers.Add("Reason", e.Message);
            }
            return responseData;
        }


    }
}
