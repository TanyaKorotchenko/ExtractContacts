using System;
using System.Collections.Generic;

namespace Domains
{
    [Serializable]
    public class ParserResult
    {
        public List<string> PhoneNumbers
        {
            get;set;
        }
        public List<string> Emails
        {
            get; set;
        }
        
    }
}
