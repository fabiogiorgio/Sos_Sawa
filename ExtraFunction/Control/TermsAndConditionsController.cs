using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ExtraFunction.Repository_.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExtraFunction.Authorization;
using ExtraFunction.Model;
using System.IO;
using ExtraFunction.DTO_;
using ExtraFunction.Service;

namespace ExtraFunction.Control
{
    public class TermsAndConditionsController
    {
        private readonly ILogger<TermsAndConditionsController> _logger;
        private ITermsAndConditionsService _termsAndConditionsService;


        public TermsAndConditionsController(ILogger<TermsAndConditionsController> log, ITermsAndConditionsService termsAndConditionsService)
        {
            _logger = log;
            _termsAndConditionsService = termsAndConditionsService;
        }

        [Function(nameof(GetTermsAndConditions))]
        [OpenApiOperation(operationId: "GetTermsAndCondition", tags: new[] { "Terms and Conditions" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(TermsAndConditions), Description = "Successfully received terms and conditions")]
        public async Task<HttpResponseData> GetTermsAndConditions([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "TermsAndCondition")] HttpRequestData req)
        {
            HttpResponseData responseData = req.CreateResponse();

            try
            {
                TermsAndConditions termsAndCondition = await _termsAndConditionsService.GetTermsAndConditions();
                responseData = req.CreateResponse(HttpStatusCode.OK);
                await responseData.WriteAsJsonAsync(termsAndCondition);
                return responseData;

            }
            catch (Exception e)
            {

                responseData.StatusCode = HttpStatusCode.BadRequest;
                responseData.Headers.Add("Reason", e.Message);
            }

            return responseData;
        }


        [Function(nameof(UpdateTermsAndConditions))]
        [OpenApiOperation(operationId: "Updates Terms and Conditions", tags: new[] { "CMS" }, Summary = "Updates Terms and Conditions", Description = "This endpoint updates terms and conditions")]
        [OpenApiRequestBody("application/json", typeof(UpdateTermsAndConditionsDTO), Description = "The terms and co data.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(UpdateTermsAndConditionsDTO), Description = "The OK response with the updated terms and conditions.")]
        public async Task<HttpResponseData> UpdateTermsAndConditions([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateTermsAndConditions")] HttpRequestData req)
        {
            _logger.LogInformation("Updating terms and co");

            HttpResponseData responseData = req.CreateResponse();

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateTermsAndConditionsDTO updateTermsAndConditionDTO = JsonConvert.DeserializeObject<UpdateTermsAndConditionsDTO>(requestBody);

            try
            {
                await _termsAndConditionsService.UpdateTermsAndConditions(updateTermsAndConditionDTO);
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
