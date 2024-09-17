using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamer.Client.ConsoleApp.Framework.Helpers
{
    public static class JsonHelper
    {

        public static string ToJson(this object source)
        {
            return JsonConvert.SerializeObject(source);
        }

    }
}
