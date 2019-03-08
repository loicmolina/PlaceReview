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
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime DatePublication { get; set; }
        public string TempsDepuisPublication { get; set; }
        public Image Photo { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public String Adresse { get; set; }

        public ReviewDisplay()
        {
            
        }

        public ReviewDisplay(string titre, string description, List<string> tag)
        {
            Titre = titre;
            Description = description;
            Tags = tag;
        }

        public Review ToReview()
        {
            return new Review(Titre, Description, Tags)
            {
                Id = this.Id,
                DatePublication = this.DatePublication,
                Longitude = this.Longitude,
                Latitude = this.Latitude,
                Adresse = this.Adresse
            };
        }
    }
}
