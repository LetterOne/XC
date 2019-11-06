using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using XC.WebAPI.Models;

namespace XC.WebAPI.Services
{
    public class MongoDbBaseService<T> where T:BaseFlowModel,new()
    {
        private readonly IMongoCollection<T> _collections;

        public MongoDbBaseService(IMongoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collections = database.GetCollection<T>(typeof(T).Name);
        }



        public List<T> Get() =>
            _collections.Find(collection => true).ToList();

        public  T Get(string id)
        {
         
             return  _collections.Find(collection=>collection.Id==id).FirstOrDefault();
      
         
        }

        public T Create(T collection)
        {
            _collections.InsertOne(collection);
            return collection;
        }

        public void Update(string id, T collectionIn)
        {
            _collections.ReplaceOne(collection => collection.Id == id, collectionIn);
        }

        public void Remove(T collectionIn) =>
            _collections.DeleteOne(collection => collection.Id == collectionIn.Id);

        public void Remove(string id) =>
            _collections.DeleteOne(collection => collection.Id == id);
    }
}
