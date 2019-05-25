using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    public class LOGIN
    {
      
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Courrier électronique")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Mot de passe")]
        
        public string Mdp { get; set; }
        public string Type { get; set; }



        public bool isConnected(int NbrCol)
        {
            if (NbrCol == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //public bool isValid(string e,string m)
        //{
        //    if (e == this.Email && this.Mdp==m)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
    }
}