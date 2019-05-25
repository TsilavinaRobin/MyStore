using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyStore.Models
{
    public enum Couleur
    {
        gris,orange,rouge,blanc,vert,bleu,noir,jaune,marron,rose,violet
    }
    //public enum Taille
    //{
    //    s, m, l, xl
    //}
    public class GENRE1
    {
        [Required]
        [DisplayName("Nom de modèle")]
        public string NomGenre { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        public string Taille { get; set; }
        [Required]
        public string Couleur { get; set; }
        [Required]
        [DisplayName("Nombre")]
        public int NbrGenre { get; set; }
        [Required]

        public decimal Prix { get; set; }
        public decimal Reduction { get; set; }

        [DisplayName("Article pour")]
        public string Pour { get; set; }



        //[Required]
        //public Couleur Couleur { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]

        public string Description { get; set; }
        public GENRE1()
        {
            this.Reduction = 0;
        }

        public decimal CalculPrix1()
        {
            decimal res = this.Prix - ((this.Prix * this.Reduction) / 100);

            return res;

        }
    }
    }