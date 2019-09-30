

using ACoreX.Extensions.Types;
using System;
using System.Collections.Generic;

namespace ACoreX.WebAPI
{
    public class CustomException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; set; }

        public int? Code { get; set; }
        public string ReferenceDocumentLink { get; set; }

        public CustomException(string message,
                            int? code = null,
                            IEnumerable<ValidationError> errors = null,
                            string refLink = "") :
            base(message)
        {
            Errors = errors;
            Code = code;
            ReferenceDocumentLink = refLink;
        }

        public CustomException(Exception ex, int? code = null) : base(ex.Message)
        {
            Code = code;
        }
    }
}
