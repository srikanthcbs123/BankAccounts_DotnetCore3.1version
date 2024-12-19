using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using System;

namespace BankAccounts_DotnetCore3._1version.CustomMiddlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
      //  private readonly IConnectionFactory _connectionFactory;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //Log The orginal error into log file
                var result = JsonConvert.SerializeObject(new
                {
                    StatusCode = response.StatusCode.ToString(),
                    ErrorMessage = error?.Message,
                    StackTraceError = error?.StackTrace?.ToString(),
                    InnerExceptionError = error?.InnerException?.ToString()
                });
                Log.Error("Custom Failure: {@StatusCode}, {@ErrorMessage}, {@StackTraceError},{@InnerExceptionError}",
                response.StatusCode.ToString(), Convert.ToString(error?.Message), Convert.ToString(error?.StackTrace), Convert.ToString(error?.InnerException));
                //here log the message in our project text file by using serilog.
                //in sqlserver database also we are logging the exceptions.
                //in Azure application insights  we are logging the exceptions
                //in Aws we are logging the exceptions in cloud watch
                //in  network log also some of the companies log the error messages.
                string connection = "data source=DESKTOP-AAO14OC;integrated security=yes;Encrypt=True;TrustServerCertificate=True;initial catalog=hotelmanagement";
                using (SqlConnection con=new SqlConnection(connection))
                {
                    SqlCommand cmd = new SqlCommand("Usp_AddProjectLevelErrorlog", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StatusCode", response.StatusCode.ToString());
                    cmd.Parameters.AddWithValue("@ErrorMessage", error?.Message);
                    cmd.Parameters.AddWithValue("@StackTraceError", error?.StackTrace?.ToString());
                    cmd.Parameters.AddWithValue("@InnerExceptionError", "");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ErrorLog");

                }
                //.......Write The logic In Future Based on Your Cloud Usage requirment.
                //If you use Azure cloud,Add the Azure Application Insights Logic Here.To Log The Exceptions in Azure cloud.
                //If You use Aws cloud Add the Aws CloudWatchLogic Here.To Log The exceptions In Aws cloud.

                var errorFriendlyMessage = new ProblemDetails
                {
                    Type = "API Exception",
                    Status = (short)HttpStatusCode.InternalServerError,
                    Title = "Internal server error occured in the api"
                };
                //while returning the message to api show user friendly error message
                var ErrorResult = JsonConvert.SerializeObject(errorFriendlyMessage);
                await response.WriteAsync(ErrorResult);
            }
        }
    }
}
