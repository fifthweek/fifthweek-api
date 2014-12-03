namespace Fifthweek.Api
{
    using System;
    using System.Linq;
    using System.Web.Http.ModelBinding;

    public class ModelValidationException : Exception
    {
        private readonly string friendlyMessage;

        public ModelValidationException(ModelStateDictionary modelState)
        {
            this.friendlyMessage = (from item in modelState.Values
                                        from error in item.Errors
                                        select error.ErrorMessage).First();

            var errorList = (from item in modelState from error in item.Value.Errors select item.Key + ": " + error.ErrorMessage).ToList();
            this.AllErrors = string.Join(Environment.NewLine, errorList);
        }

        public override string Message
        {
            get
            {
                return this.friendlyMessage;
            }
        }

        public string AllErrors { get; private set; }

        public override string ToString()
        {
            return this.AllErrors + Environment.NewLine + base.ToString();
        }
    }
}