using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public class VENDEUR1
    {
        [Required]
        public string Nom { get; set; }

        public string Prenom { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Date de naissance")]
        public string DateNaiss { get; set; }
        [Required]
        public string Magasin { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Courrier électronique")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Mot de passe")]
        [StringLength(100, ErrorMessage = "La chaîne {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        public string Mdp { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirmation de mot de passe")]
        [Compare("Mdp", ErrorMessage = "Le nouveau mot de passe et le mot de passe de confirmation ne correspondent pas.")]
        public string CMdp { get; set; }
        [Required]
        [DisplayName("N°Telephone")]
        public string Contact { get; set; }
        [Required]
        [DisplayName("Lieu")]
        public string Lieu { get; set; }

        public bool isValid()
        {
            if (this.CMdp == this.Mdp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
