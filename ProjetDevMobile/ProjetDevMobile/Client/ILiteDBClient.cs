using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetDevMobile.Client
{
    interface ILiteDBClient
    {
        List<TObject> GetCollectionFromDB<TObject>(string collectionName) where TObject : class;
        void InsertObjectInDB<TObject>(TObject objectToInsert, string collectionName) where TObject : class;
        void RemoveObjectFromDB<TObject>(int objectToRemoveID, string collectionName) where TObject : class;
        void CleanCollection(string collectionName);
    }
}
