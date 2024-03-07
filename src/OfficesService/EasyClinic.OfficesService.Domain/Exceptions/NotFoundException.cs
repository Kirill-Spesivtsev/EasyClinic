﻿using System.Net;


namespace EasyClinic.OfficesService.Domain.Exceptions
{
    public class NotFoundException : HttpResponseCodeException
    {
        public NotFoundException(string message) : base(message)
        {
            Status = (int)HttpStatusCode.NotFound;
            Title = "Not Found";
            Type = @"https://tools.ietf.org/html/rfc7231#section-6.5.4";
        }
    }
}
