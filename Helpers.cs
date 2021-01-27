using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace APITesting
{
    public class Helpers
    {
      
        public string UriHelper(string fragment, string path)
        {
            UriBuilder uri = new UriBuilder(fragment);
            uri.Path = path;
            uri.Port = -1;

            return uri.ToString();
        }
    }
}
