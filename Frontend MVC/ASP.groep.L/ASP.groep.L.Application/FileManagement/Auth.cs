using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.groep.L.Application.FileManagement
{
    public class Auth
    {
        public string APIKey { get; set; }

        public Auth(string apiKey)
        {
            APIKey = apiKey;
        }
    }
}
