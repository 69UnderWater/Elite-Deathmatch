using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;

namespace Gangwar
{
    class Http
    {
        public static byte[] Post(string url, NameValueCollection pairs)
        {
            using (WebClient WebClient = new WebClient())
            {
                return WebClient.UploadValues(url, pairs);
            }
        }
    }
}
