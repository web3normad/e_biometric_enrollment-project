using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIMASASEAFARERS
{
    class IDCardClass
    {
        public static string DID;
        public static string DockworkerID
        {
            get{ return DID; }
            set{ DID = value; }
        }
    }
}
