using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class PRODUIT1
    {
        [Required]
        [DisplayName("Nom du produit")]
        public string NomPro { get; set; }
    }
}