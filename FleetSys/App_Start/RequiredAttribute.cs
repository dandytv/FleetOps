using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FleetOps.App_Start
{
    public class CustomValidationMessageAttribute:ValidationAttribute
    {
        public CustomValidationMessageAttribute()
    {
        this.ErrorMessage = "*";
    }

    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
    {
        yield return new ModelClientValidationRule
        {
            ErrorMessage = this.ErrorMessage,
            ValidationType = "required"
        };
    }
    }
}