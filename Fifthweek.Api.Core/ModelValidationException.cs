using System;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace Fifthweek.Api.Core
{
    using Fifthweek.Shared;

    public class ModelValidationException : RecoverableException
    {
        public ModelValidationException(ModelStateDictionary modelState)
            : base((from item in modelState.Values
                    from error in item.Errors
                    select error.ErrorMessage).First())
        {
            var errorList = (from item in modelState from error in item.Value.Errors select item.Key + ": " + error.ErrorMessage).ToList();
            this.AllErrors = string.Join(Environment.NewLine, errorList);
        }

        public string AllErrors { get; private set; }

        public override string ToString()
        {
            return this.AllErrors + Environment.NewLine + base.ToString();
        }
    }
}