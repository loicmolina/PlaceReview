using System;
using System.Collections.Generic;
using System.Text;
using ProjetDevMobile.Client;
using ProjetDevMobile.Model;

namespace ProjetDevMobile.Services
{
    class ReviewService : IReviewService
    {
        private List<Review> _reviews;

        private ILiteDBClient _liteDBClient;
        private string _dbCollectionReview = "collectionReview";

        public ReviewService(ILiteDBClient liteDBClient)
        {
            _liteDBClient = liteDBClient;

            _reviews = new List<Review>();

            Init();
        }

        private void Init()
        {
            _reviews = _liteDBClient.GetCollectionFromDB<Review>(_dbCollectionReview);
        }

        public void AddReview(Review review)
        {
            _liteDBClient.InsertObjectInDB<Review>(review, _dbCollectionReview);
            _reviews.Add(review);
        }

        public void DeleteReview(Review review)
        {
            _liteDBClient.RemoveObjectFromDB<Review>(review.Id, _dbCollectionReview);
            _reviews.Remove(review);
        }

        public Review GetReview(int pos)
        {
            return _reviews[pos];
        }

        public List<Review> GetReviews()
        {
            return _reviews;
        }
    }
}
