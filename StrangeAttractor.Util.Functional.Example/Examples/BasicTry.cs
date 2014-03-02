using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
