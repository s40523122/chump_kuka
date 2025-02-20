using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iCAPS;

namespace Chump_kuka
{
    public class Env : iCAPS.Env
    {
        public static HttpRequest kuka_api = new HttpRequest("http://192.168.68.64:10870/interfaces/api/amr/");
        public static HttpRequest pmc_api = new HttpRequest("http://127.0.0.1:10870");

        public static bool enble_kuka_api = false;
    }
}
