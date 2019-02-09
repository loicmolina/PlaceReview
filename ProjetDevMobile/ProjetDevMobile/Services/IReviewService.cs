using System;
using System.Collections.Generic;
using System.Text;
using ProjetDevMobile.Model;

namespace ProjetDevMobile.Services
{
    interface IReviewService
    {
        List<Review> GetReviews();

        void AddReview(Review review);

        void DeleteReview(Review review);

        Review GetReview(int pos);
    }
}
