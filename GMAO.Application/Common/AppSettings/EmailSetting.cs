using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.AppSettings
{
    public class EmailSetting
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string User { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
    }
}
