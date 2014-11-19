namespace Fifthweek.Api
{
    using System;
    using System.Diagnostics;
    using System.Text;

    using Autofac.Core;

    public class LogRequestModule : Autofac.Module
    {
        public static int depth = 0;

        protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += this.RegistrationOnPreparing;
            registration.Activating += this.RegistrationOnActivating;
            base.AttachToComponentRegistration(componentRegistry, registration);
        }

        private string GetPrefix()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < depth; i++)
            {
                sb.Append("-");
            }

            return sb.ToString();
        }

        private void RegistrationOnPreparing(object sender, PreparingEventArgs preparingEventArgs)
        {
            Trace.Write(this.GetPrefix());
            Trace.Write("Resolving ");
            Trace.Write(preparingEventArgs.Component.Activator.LimitType);
            Trace.WriteLine(String.Empty);
            depth++;
        }

        private void RegistrationOnActivating(object sender, ActivatingEventArgs<object> activatingEventArgs)
        {
            depth--;
            Trace.Write(this.GetPrefix());
            Trace.Write("Activating ");
            Trace.Write(activatingEventArgs.Component.Activator.LimitType);
            Trace.WriteLine(String.Empty);
        }
    }
}