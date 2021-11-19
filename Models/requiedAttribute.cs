using System;

namespace LTQL.Models
{
    internal class RequiedAttribute : Attribute
    {
        public string ErrorMessage { get; set; }
    }
}