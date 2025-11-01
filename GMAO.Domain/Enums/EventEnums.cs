using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Enums
{
    public enum EventPriority
    {
        Low,
        Normal,
        High,
        Critical
    }

    public enum Origin
    {
        None = 0,
        PhoneCall = 1,
        AnsweringMachine = 2,
        Internal = 3,
        Fax = 4,
        Mail = 5,
        Radio = 6,
        Website = 7,
        Email = 8
    }

    public enum Interlocutor
    {
        None = 0,
        Internal = 1,
        ClientContact = 2,
        SiteKeeper = 3,
        SiteContact = 5,
        Other = 6
    }

    public enum ServiceOrderSource
    {
        None = 0,
        Origin = 1,
        File = 2
    }

   
}
