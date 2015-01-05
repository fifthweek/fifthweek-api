namespace Fifthweek.Api.Core
{
    using System;
    using System.Runtime.InteropServices;

    public class GuidCreator : IGuidCreator
    {
        private const int RPC_S_OK = 0;
        private const int RPC_S_UUID_LOCAL_ONLY = 1824;
        private const int RPC_S_UUID_NO_ADDRESS = 1739;

        public Guid Create()
        {
            return Guid.NewGuid();
        }

        public Guid CreateClrSequential()
        {
            Guid guid;
            int rc = UuidCreateSequential(out guid);

            // See comments in http://sqlblog.com/blogs/alberto_ferrari/archive/2007/08/31/how-are-guids-sorted-by-sql-server.aspx
            switch (rc)
            {
                case RPC_S_OK:
                    return guid;

                case RPC_S_UUID_LOCAL_ONLY:
                    throw new NotImplementedException("OS returned RPC_S_UUID_LOCAL_ONLY generating sequential GUID: " + rc);

                case RPC_S_UUID_NO_ADDRESS:
                    throw new NotImplementedException("OS returned RPC_S_UUID_NO_ADDRESS generating sequential GUID: " + rc);

                default:
                    throw new NotImplementedException("OS returned unexpected error code generating sequential GUID: " + rc);
            }
        }

        public Guid CreateSqlSequential()
        {
            // See: http://blogs.msdn.com/b/dbrowne/archive/2012/07/03/how-to-generate-sequential-guids-for-sql-server-in-net.aspx
            Guid guid = this.CreateClrSequential();
            var s = guid.ToByteArray();
            var t = new byte[16];
            t[3] = s[0];
            t[2] = s[1];
            t[1] = s[2];
            t[0] = s[3];
            t[5] = s[4];
            t[4] = s[5];
            t[7] = s[6];
            t[6] = s[7];
            t[8] = s[8];
            t[9] = s[9];
            t[10] = s[10];
            t[11] = s[11];
            t[12] = s[12];
            t[13] = s[13];
            t[14] = s[14];
            t[15] = s[15];
            return new Guid(t);
        }

        [DllImport("rpcrt4.dll", SetLastError = true)]
        private static extern int UuidCreateSequential(out Guid guid);
    }
}