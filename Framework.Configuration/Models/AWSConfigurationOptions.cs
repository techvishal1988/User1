using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Configuration.Models
{
    public class AWSConfigurationOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string UserPoolId { get; set; }

        public string UserPoolClientId { get; set; }
    }
}
