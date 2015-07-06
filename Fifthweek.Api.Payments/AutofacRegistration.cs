namespace Fifthweek.Api.Payments
{
    using Autofac;

    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Stripe;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GetUserPaymentOriginDbStatement>().As<IGetUserPaymentOriginDbStatement>();
            builder.RegisterType<SetUserPaymentOriginDbStatement>().As<ISetUserPaymentOriginDbStatement>();
            builder.RegisterType<CreateStripeCustomer>().As<ICreateStripeCustomer>();
            builder.RegisterType<UpdateStripeCustomerCreditCard>().As<IUpdateStripeCustomerCreditCard>();
            builder.RegisterType<PerformStripeCharge>().As<IPerformStripeCharge>();
            builder.RegisterType<SaveCustomerCreditToLedgerDbStatement>().As<ISaveCustomerCreditToLedgerDbStatement>();
            builder.RegisterType<GetTaxInformation>().As<IGetTaxInformation>();
            builder.RegisterType<CreateTaxamoTransaction>().As<ICreateTaxamoTransaction>();
            builder.RegisterType<DeleteTaxamoTransaction>().As<IDeleteTaxamoTransaction>();
            builder.RegisterType<CommitTaxamoTransaction>().As<ICommitTaxamoTransaction>();
            builder.RegisterType<PerformCreditRequest>().As<IPerformCreditRequest>();
            builder.RegisterType<CommitCreditToDatabase>().As<ICommitCreditToDatabase>();
            builder.RegisterType<InitializeCreditRequest>().As<IInitializeCreditRequest>();
        }
    }
}