using System;
using Domains;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ExtractContactData
{
    public class Parser
    {


        public ParserResult Parse(string text)
        {
            var result = new ParserResult();
            string phoneTemplate = @"(\+?\d{10,12})|(\+?\d{3}(\(|\s{1,2})?\d{3}(\)|\s{1,2})?\d{6,9})|((\d+(\s|-)?){5})";
            Regex regex = new Regex(phoneTemplate);
            result.PhoneNumbers = new List<string>();
            var phoneNumbers=new List<string>();
            foreach (Match match in regex.Matches(text))
            {
               phoneNumbers.Add(match.Value.Replace(" ","").Replace("-","").Replace("+","").Replace("(","").Replace(")","").Replace("\r",""));
            }
            phoneNumbers = phoneNumbers.Distinct().ToList();
            var formattedphoneNumbers = new List<string>();
            foreach (var phoneNumber in phoneNumbers)
            {
                //string.Format("")
                Regex r = new Regex(@"^((380)|(80)|(0))\d+");

                foreach (Match match in r.Matches(phoneNumber))
                {
                    int len = match.Groups[1].Value.Length;
                    //phoneNumber.Substring(len, phoneNumber.Length- len)
                    formattedphoneNumbers.Add($"+380{phoneNumber.Substring(len, phoneNumber.Length - len)}");
                }
            }
            result.PhoneNumbers = formattedphoneNumbers.Distinct().ToList();
            Regex reg = new Regex(@"[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,6}", RegexOptions.IgnoreCase);
            

            List<string> emails = new List<string>();
            for (var match = reg.Match(text); match.Success; match = match.NextMatch())
            {
                if (!(emails.Contains(match.Value)))
                    emails.Add(match.Value);
            }
            result.Emails = emails.Distinct().ToList();
            return result;
        }

        public string ParseAndReturnAsText(string text)
        {
            return JsonConvert.SerializeObject(Parse(text));
        }
    }
}
