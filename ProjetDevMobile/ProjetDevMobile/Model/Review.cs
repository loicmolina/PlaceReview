using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using ProjetDevMobile.Utils;

namespace ProjetDevMobile.Model
{
    public enum ReviewTypes { Drink, Food, ToSee }

    public class Review
    {        
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; } = new List<string>();    
        public byte[] Photo { get; set; }
        public DateTime DatePublication { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public String Adresse { get; set; }

        public Review()
        {

        }

        public Review(string titre, string description, List<string> tag)
        {
            Titre = titre;
            Description = description;
            Tags = tag;
        }

        public ReviewDisplay ToReviewDisplay()
        {
            return new ReviewDisplay(Titre, Description, Tags)
            {
                Id = this.Id,
                Photo = ImageUtils.ByteArrayToImage(this.Photo),
                Longitude = this.Longitude,
                Latitude = this.Latitude,
                Adresse = this.Adresse,
                DatePublication = this.DatePublication,
                TempsDepuisPublication = TempsUtils.DatePublicationToTemps(DatePublication)
            };
        }
    }
}
