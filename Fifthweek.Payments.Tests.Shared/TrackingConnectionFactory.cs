namespace Fifthweek.Payments.Tests.Shared
{
    using System;
    using System.Data.Common;

    using Fifthweek.Api.Persistence;

    public class TrackingConnectionFactory : IFifthweekDbConnectionFactory
    {
        private readonly IFifthweekDbConnectionFactory child;

        public TrackingConnectionFactory(IFifthweekDbConnectionFactory child)
        {
            this.child = child;
        }

        public bool ConnectionDisposed { get; private set; }

        public DbConnection CreateConnection()
        {
            var connection = this.child.CreateConnection();
            connection.Disposed += this.OnConnectionDisposed;
            return connection;
        }

        public FifthweekDbContext CreateContext()
        {
            var context = this.child.CreateContext();
            context.Database.Connection.Disposed += this.OnConnectionDisposed;
            return context;
        }

        private void OnConnectionDisposed(object sender, EventArgs e)
        {
            this.ConnectionDisposed = true;
        }
    }
}