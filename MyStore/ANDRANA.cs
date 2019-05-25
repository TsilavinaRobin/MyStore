using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore
{
    public class ANDRANA
    {
       static public bool Nom { get; set; }
        static public string Type { get; set; }
        static public string Name { get; set; }
        static public string Autorisation { get; set; }
        static public string[] Boutique  { get; set; }
       static public int  IDUSER { get; set; }
        static public int IDCAT { get; set; }
        static public int IDPROD { get; set; }
        static public int IDPANIER { get; set; }

        public ANDRANA()
        {
            Nom = false;
            Type = "Aucun";
            Name = "Inconnu";
            Autorisation = "false";
            IDUSER = 20;
            IDCAT = 20;
            IDPROD = 20;
            IDPANIER = 20;

        }
    }
}