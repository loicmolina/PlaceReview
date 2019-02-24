using ProjetDevMobile.Utils;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Text;

namespace ProjetDevMobile.Model
{
    public class ReviewDisplay
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public DateTime DatePublication { get; set; }
        public Image Photo { get; set; }
        public float Longitute { get; set; }
        public float Latitude { get; set; }

        public ReviewDisplay(string titre, string description, string tag)
        {
            Titre = titre;
            Description = description;
            Tag = tag;
        }

        public Review ToReview()
        {
            Review review = new Review(Titre, Description, Tag)
            {
                DatePublication = this.DatePublication,
                Longitute = this.Longitute,
                Latitude = this.Latitude
            };
            return review;
        }
    }
}
