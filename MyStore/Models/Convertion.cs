using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public static class Convertion
    {
        public static double PrixMg;
        public static double PrixUS;

        public static double convertir(double PrixMg)
        {
           double PrixUS = PrixMg / 4000;
            return PrixUS;
        }
        public static double AvecTaxe(double PrixMg)
        {
        double PrixUS = PrixMg / 4000;

            double resultat = PrixUS + 2;
            return resultat;
        }
        public static double Plus2(double total)
        {
            double res = total + 2;
            return res ;
        }
    }
    //public static double somme(List<GENRE> PanLi)
    //{
    //    double res = 0;
    //    foreach(var hf in PanLi)
    //    {
    //      res = +Convert.ToDouble(hf.Prix);
           
    //    }
    //    return res;
    //}
   
}