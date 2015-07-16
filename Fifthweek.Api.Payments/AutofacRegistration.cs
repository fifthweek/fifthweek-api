namespace Fifthweek.Api.Payments
{
    using Autofac;

    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Stripe;
    using Fifthweek.Payments.Services.Credit.Taxamo;
    using Fifthweek.Shared;

    using CommitTestUserCreditToDatabase = Fifthweek.Payments.Services.Credit.CommitTestUserCreditToDatabase;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<SetUserPaymentOriginDbStatement>().As<ISetUserPaymentOriginDbStatement>();
            builder.RegisterType<CommitTestUserCreditToDatabase>().As<ICommitTestUserCreditToDatabase>();
            builder.RegisterType<Fifthweek.Payments.Services.Credit.SetTestUserAccountBalanceDbStatement>().As<ISetTestUserAccountBalanceDbStatement>();
        }
    }
}