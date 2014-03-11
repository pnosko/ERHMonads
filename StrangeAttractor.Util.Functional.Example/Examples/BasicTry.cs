using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrangeAttractor.Util.Functional.Interfaces;
using StrangeAttractor.Util.Functional.Singletons;
using StrangeAttractor.Util.Functional.Extensions;

namespace StrangeAttractor.Util.Functional.Example.Examples
{
    class BasicTry
    {
        public void Run()
        {
            string input = "10";
            int count;

            if (!int.TryParse(input, out count))
            {
                count = -1;
            }
        }


        public static byte[] FetchDataaaa(string url)
        {
            // throws HttpRequestException and TimoutException
            return new byte[0];
        }

        public int GetInt()
        {
            return 10;
        }

        public IOption<int> ReadIntOption()
        {
            int value;
            if (int.TryParse(Console.ReadLine(), out value))
            {
                return value.ToOption();
            }
            return Option.Nothing<int>();
        }
    }
}
