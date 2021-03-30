#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ReversiRestAPI.Helpers
{
    public class ResponseHelper
    {
        private ControllerBase cBase;

        public ResponseHelper(ControllerBase cBase)
        {
            this.cBase = cBase;
        }

        public ActionResult GetStatusCode(HttpStatusCode code, string message, bool error = false)
        {
            var response = new ResponseMessage();
            if (!error)
                response.Message = message;
            else
                response.Error = message;

            return cBase.StatusCode((int) code, JsonConvert.SerializeObject(
                response, 
                new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore}
            ));
        }

        public class ResponseMessage
        {

            public string? Message { get; set; }
            public string? Error { get; set; }
        }
    }
}
