using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;

namespace StrangeAttractor.Util.Functional.Extensions
{
    public static class TaskExtensions
    {
        public static void OnComplete<T>(this Task<T> self, Action<ITry<T>> handler)
        {
            self.ContinueWith(x => handler(Try.FromTask(x)));
        }
    }
}
