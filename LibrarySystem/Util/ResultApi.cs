using LibrarySystem.Data;
using LibrarySystemModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace LibrarySystem.Util
{
    public static class ResultApi
    {
        public static ContentResult ModelNotValid()
        {
            return new ContentResult
            {
                Content = DataUtil.ModelNotValid,
                ContentType = "text/plain",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        public static ContentResult CreateData(int id)
        {
            return new ContentResult
            {
                Content = id.ToString(),
                ContentType = "text/plain",
                StatusCode = (int)HttpStatusCode.OK

            };
        }

        public static ContentResult Succeeded()
        {
            return new ContentResult
            {
                Content = "true",
                ContentType = "text/plain",
                StatusCode = (int)HttpStatusCode.OK
            };
        }

        public static ContentResult Failed()
        {
            return new ContentResult
            {
                Content = "false",
                ContentType = "text/plain",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        
    }
}
