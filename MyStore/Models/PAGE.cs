using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class PAGE
    {
        static public int NumPage;
        static public int TabMax;
        static public int TabMin;
        static public int TabTotal;
       
        public PAGE()
        {
            NumPage = 1;
            TabMax = 6;
            TabMin = 0;
            TabTotal = 1;

        }

    }
}