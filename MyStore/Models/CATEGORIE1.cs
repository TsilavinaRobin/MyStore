using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class CATEGORIE1
    {
        [Required]
        [DisplayName("Catégorie")]
        public string NomCat { get; set; }
        
    }
}