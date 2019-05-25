using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class ACHETEUR1
    {
        [Required]
        public string Nom { get; set; }

        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date de naissance")]

        public string DateNaiss { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        [DisplayName("N°Telephone")]
        public string NumTel { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Courrier electronique")]
        public string Email { get; set; }
        public string Livraison { get; set; }
    }
}