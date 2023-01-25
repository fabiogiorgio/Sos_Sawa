using ExtraFunction.Model;
using ExtraFunction.Repository_.Interface;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExtraFunction.Service;
using ExtraFunction.Repository.Interface;
using Microsoft.Azure.Documents.ChangeFeedProcessor.Logging;
using Microsoft.OpenApi.Models;
using ExtraFunction.Authorization;
using ExtraFunction.DTO;

namespace ExtraFunction.Control
{
    public class AdminController
    {
        private readonly ILogger logger;
        private ITokenService tokenService;
        private IAdminLoginService _adminLoginService;
        private IAdminService _adminService;

        public AdminController(ILogger<AdminController> logger, ITokenService tokenService, IAdminService adminService, IAdminLoginService adminLoginService)
        {
            this.logger = logger;
            this.tokenService = tokenService;
            this._adminService = adminService;
            this._adminLoginService = adminLoginService;
        }

        [Function("Login")]
        [OpenApiOperation(operationId: "Login", tags: new[] { "CMS" }, Summary = "Login for an admin",
                        Description = "This method logs in the admin for cms, and retrieves a JWT bearer token.")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(AdminLogin), Required = true, Description = "The admin credentials")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(LoginResult), Description = "Login success")]
        public async Task<HttpResponseData> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req, FunctionContext executionContext)
        {
            AdminLogin login = JsonConvert.DeserializeObject<AdminLogin>(await new StreamReader(req.Body).ReadToEndAsync());

            var resultLogin = await _adminLoginService.CheckIfCredentialsCorrect(login.username, login.password);

            if (resultLogin.ok)
            {
                LoginResult result = await tokenService.CreateAdminToken(login);
                logger.LogInformation(resultLogin.id.ToString());
                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);

                await response.WriteAsJsonAsync(resultLogin.id.ToString());
                return response;
            }
            else
            {

                HttpResponseData response = req.CreateResponse(HttpStatusCode.BadRequest);
                return response;
            }

        }
        
        [Function("CreateAdmin")]
        [OpenApiOperation(operationId: "CreateAdmin", tags: new[] { "CMS" }, Summary = "Create an admin account", Description = "This endpoint creates an admin account")]
        [OpenApiRequestBody("application/json", typeof(CreateAdminDTO), Description = "The admin data.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(CreateAdminDTO), Description = "The OK response with the new Admin.")]
        public async Task<HttpResponseData> CreateAdmin([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
        {
            HttpResponseData responseData = req.CreateResponse();
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                CreateAdminDTO adminDTO = JsonConvert.DeserializeObject<CreateAdminDTO>(requestBody);
                await _adminService.CreateAdmin(adminDTO);
                responseData.StatusCode = HttpStatusCode.Created;
                responseData.Headers.Add("Result", $"Admin has been created");
            }
            catch (Exception e)
            {
                responseData.StatusCode = HttpStatusCode.BadRequest;
                responseData.Headers.Add("Reason", e.Message);
            }
            return responseData;
        }

        [Function("UpdateAdmin")]
        [OpenApiOperation(operationId: "UpdatesAdmin", tags: new[] { "CMS" }, Summary = "Update admin by id", Description = "This endpoint update admin by id")]
        [OpenApiRequestBody("application/json", typeof(UpdateAdminDTO), Description = "The admin data.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json", bodyType: typeof(UpdateAdminDTO), Description = "The OK response with the updated admin.")]
        public async Task<HttpResponseData> UpdateAdmin([HttpTrigger(AuthorizationLevel.Anonymous, "put")] HttpRequestData req)           
        {
            HttpResponseData responseData = req.CreateResponse();


            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UpdateAdminDTO updateAdminDTO = JsonConvert.DeserializeObject<UpdateAdminDTO>(requestBody);
            try
            {
                await _adminService.UpdateAdminPassword(updateAdminDTO);
                
                responseData.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                responseData.StatusCode = HttpStatusCode.BadRequest;
                responseData.Headers.Add("Reason", e.Message);
            }
            return responseData;
        }


        //[Function(nameof(GetAllAdmins))]
        //[OpenApiOperation(operationId: "Getting all admins", tags: new[] { "CMS" }, Summary = "Get all admins", Description = "Gets all admins")]
        //public async Task<HttpResponseData> GetAllAdmins([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "admins")] HttpRequestData req)
        //{
        //    HttpResponseData responseData = req.CreateResponse();
        //    try
        //    {
        //        IEnumerable<GetAllAdminsDTO> getAllAdminsDTOs = await _adminService.GetAllAdmins();
        //        await responseData.WriteAsJsonAsync(getAllAdminsDTOs);
        //        responseData.StatusCode = HttpStatusCode.OK;
        //        return responseData;
        //    }
        //    catch (Exception e)
        //    {
        //        responseData.StatusCode = HttpStatusCode.BadRequest;
        //        responseData.Headers.Add("Reason", e.Message);
        //    }
        //    return responseData;
        //}
    }


}

