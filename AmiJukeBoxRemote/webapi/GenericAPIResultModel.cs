using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiJukeboxRemote.webapi
{
    public class GenericAPIResultModel
    {
        public GenericAPIResultModel()
        {
            Success = true;
        }

        public GenericAPIResultModel(string errorMessage, int? id = null)
        {
            Success = false;
            Message = errorMessage;
            Id = id;
        }

        public GenericAPIResultModel(string errorMessage)
        {
            Success = string.IsNullOrEmpty(errorMessage) ? true : false;
            Message = errorMessage;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public int? Id { get; set; }
    }
}