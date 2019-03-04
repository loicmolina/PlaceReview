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
        public string Tag { get; set; }
        public byte[] Photo { get; set; }
        public DateTime DatePublication { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }

        public Review()
        {

        }

        public Review(string titre, string description, string tag)
        {
            Titre = titre;
            Description = description;
            Tag = tag;
        }

        public ReviewDisplay ToReviewDisplay()
        {
            return new ReviewDisplay(Titre, Description, Tag)
            {
                Id = this.Id,
                Photo = ImageUtils.ByteArrayToImage(this.Photo),
                Longitude = this.Longitude,
                Latitude = this.Latitude,
                DatePublication = this.DatePublication,
                TempsDepuisPublication = TempsUtils.DatePublicationToTemps(DatePublication)
            };
        }
    }
}
