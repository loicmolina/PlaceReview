using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ProjetDevMobile.Client;
using ProjetDevMobile.Model;
using static ProjetDevMobile.Model.Review;

namespace ProjetDevMobile.Services
{
    public class ReviewService : IReviewService
    {
        private List<Review> _reviews;
        private List<Review> _resultReviews;

        //private ILiteDBClient _liteDBClient;
        private string _dbCollectionReview = "collectionReview";

        public ReviewService()//ILiteDBClient liteDBClient)
        {
            //_liteDBClient = liteDBClient;

            _reviews = new List<Review>();
            _resultReviews = new List<Review>();

            Init();
        }

        private void Init()
        {
            //_reviews = _liteDBClient.GetCollectionFromDB<Review>(_dbCollectionReview);
        }

        public void AddReview(Review review)
        {
            //_liteDBClient.InsertObjectInDB<Review>(review, _dbCollectionReview);
            _reviews.Add(review);
        }

        public void DeleteReview(Review review)
        {
            //_liteDBClient.RemoveObjectFromDB<Review>(review.Id, _dbCollectionReview);
            _reviews.Remove(review);
        }

        public Review GetReview(int pos)
        {
            return _reviews[pos];
        }

        public List<Review> GetReviews(bool food, bool drink, bool toSee)
        {
            _resultReviews.Clear();
            if (food)
            {
                _resultReviews.AddRange(_reviews.Where(review => review.Tag.Equals(ReviewTypes.Food.ToString())));
            }

            if (drink)
            {
                _resultReviews.AddRange(_reviews.Where(review => review.Tag.Equals(ReviewTypes.Drink.ToString())));
            }


            if (toSee)
            {
                _resultReviews.AddRange(_reviews.Where(review => review.Tag.Equals(ReviewTypes.ToSee.ToString())));
            }

            return _resultReviews;
        }
    }
}
