using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Model
{
    public class Review
    {
        public enum ReviewTypes { Drink, Food, ToSee }
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string Photo { get; set; } //TODO: modifier le type
        public float Longitute { get; set; }
        public float Latitude { get; set; }

        public Review(string titre, string description, string tag)
        {
            Titre = titre;
            Description = description;
            Tag = tag;
        }

    }
}
