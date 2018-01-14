using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions.Util
{
    public static class Helper
    {

    }

    public class CheckExecution : IDisposable
    {
        Stopwatch watch;
        public string MethodName { get; set; }
        public CheckExecution(string methodName)
        {
            MethodName = methodName;
            watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            watch.Stop();
            Console.WriteLine($"{MethodName} took {watch.ElapsedMilliseconds}");
        }
    }
}
