using System;
using System.Collections.Generic;
using System.Text;
using ProjetDevMobile.Model;

namespace ProjetDevMobile.Services
{
    public interface IReviewService
    {
        List<Review> GetReviews(bool food, bool drink, bool toSee);

        void AddReview(Review review);

        void DeleteReview(Review review);

        Review GetReview(int pos);
    }
}
