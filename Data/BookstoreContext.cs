using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Cluster0.Models;
namespace Cluster0.Data
{
    public class BookstoreContext
    {
    private readonly IMongoDatabase _database;

    public BookstoreContext(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<Book> Books => _database.GetCollection<Book>("books");
    }
}