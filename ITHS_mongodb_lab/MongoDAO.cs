using ITHS_mongodb_lab.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace ITHS_mongodb_lab;

/// <summary>
/// Communicates with database with ODMs
/// </summary>
public class MongoDAO: IItemDAO
{
    // A Mongodb-collection of ODM - objects to create DAO-object from.
    IMongoCollection<ItemODM> collection;

    MongoClient mongoClient;
    IMongoDatabase database;


    // Connect to cluster and database, via ui
    public MongoDAO(string connectionString, IStringIO io)
    {   
        // Cluster connection
        this.mongoClient = new MongoClient(connectionString);

        // Database connection - takes a list of database names and passes to the io method to
        // choose which database to connect to, then connects to it.
        this.database = mongoClient.GetDatabase(mongoClient.ListDatabaseNames().ToList()[io.ShowMenuAndGetChoise(mongoClient.ListDatabaseNames().ToList(), "database") - 1].ToString());

        // Same as above, but for connecting to a collection that the user chooses from a list.
        this.collection = this.database.GetCollection<ItemODM>(this.database.ListCollectionNames().ToList()[io.ShowMenuAndGetChoise(this.database.ListCollectionNames().ToList(), "collection") - 1]);          
    }


    /// <summary>
    /// Create new document/item with InsertOne.
    /// </summary>
    /// <param name="item"></param>
    public void CreateItem(ItemODM item)
    {
        collection.InsertOne(item);
    }


    public List<ItemODM> ReadAllItems()
    {        
        return this.collection.Find(new BsonDocument()).ToList();
    }


    /// <summary>
    /// The method takes filter and update parameters to be more dynamic.
    /// </summary>
    /// <param name="filter">ItemODM-filter representing what document to update.</param>
    /// <param name="update">Information about what field to update.</param>
    public void UpdateItem(FilterDefinition<ItemODM> filter, UpdateDefinition<ItemODM> update)
    {
        collection.UpdateOne(filter, update);
    }
    
    /// <summary>
    /// Takes an id, creates a filter from it and calls the delete-method.
    /// </summary>
    /// <param name="objectId"></param>
    public void DeleteItem(ObjectId objectId)
    {
        var filter = Builders<ItemODM>.Filter.Eq("_id", objectId);
        collection.DeleteOne(filter);
    }
}