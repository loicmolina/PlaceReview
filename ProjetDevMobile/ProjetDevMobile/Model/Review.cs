using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Model
{
    class Review
    {
        public enum ReviewTypes { Drink, Food, ToSee }
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public ReviewTypes TypeReview { get; set; }
        public string Photo { get; set; } //TODO: modifier le type
        public float Longitute { get; set; }
        public float Latitude { get; set; }

        public Review(string titre, string description, ReviewTypes typeReview)
        {
            Titre = titre;
            Description = description;
            TypeReview = typeReview;
        }

    }
}
