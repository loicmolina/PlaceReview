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
        public string TempsDepuisPublication { get; set; }
        public Image Photo { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public ReviewDisplay()
        {    }

        public ReviewDisplay(string titre, string description, string tag)
        {
            Titre = titre;
            Description = description;
            Tag = tag;
        }

        public Review ToReview()
        {
            return new Review(Titre, Description, Tag)
            {
                Id = this.Id,
                DatePublication = this.DatePublication,
                Longitude = this.Longitude,
                Latitude = this.Latitude
            };
        }
    }
}
