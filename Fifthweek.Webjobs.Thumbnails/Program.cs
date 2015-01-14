using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fifthweek.Webjobs.Thumbnails
{
    using Microsoft.Azure.WebJobs;

    class Program
    {
        static void Main(string[] args)
        {
            var host = new JobHost();
            host.RunAndBlock();
        }
    }
}
