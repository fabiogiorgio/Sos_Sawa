using ExtraFunction.Repository_.Interface;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Documents.ChangeFeedProcessor.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExtraFunction.DTO;
using ExtraFunction.Model;
using System.Collections;

namespace ExtraFunction.Control
{
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [Function(nameof(DeactivateUser))]
        [OpenApiOperation(operationId: "DeactivateUser", tags: new[] { "CMS" }, Summary = "Activate/Deactivate user account", Description = "This endpoint Activate/Deactivate user accountby id")]
        [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid), Description = "The user ID parameter")]
        [OpenApiParameter(name: "isAccountActive", In = ParameterLocation.Path, Required = true, Type = typeof(bool), Description = "Deactivates an user - NO COMING BACK")]
        public async Task<HttpResponseData> DeactivateUser([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users/{userId:Guid}/{isAccountActive:bool}")] HttpRequestData req, Guid userId, bool isAccountActive, FunctionContext functionContext)
        {
            _logger.LogInformation($"Fetching the user by id {userId}");
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                await _userService.DeactivateUserAccount(userId, isAccountActive);
                responseData.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                responseData.StatusCode = HttpStatusCode.BadRequest;
                responseData.Headers.Add("Reason", e.Message);
            }
            return responseData;
        }
        [Function(nameof(GetAllUsers))]//change tags for all cms endpoints
        [OpenApiOperation(operationId: "Getting all users", tags: new[] { "CMS" }, Summary = "Get all users", Description = "Gets all users")]
        public async Task<HttpResponseData> GetAllUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req)
        {
            _logger.LogInformation($"Getting all users");
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                IEnumerable<GetAllUsersCmsDTO> getAllUsersCmsDTOs = await _userService.GetAllUsers();
                await responseData.WriteAsJsonAsync(getAllUsersCmsDTOs);
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
