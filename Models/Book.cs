using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace Cluster0.Models
{
    public class Book
    {
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement("book_name")]
    public string BookName { get; set; }

    [BsonElement("author")]
    public string Author { get; set; }

    [BsonElement("rating")]
    public double Rating { get; set; }

    [BsonElement("date_added")]
    public DateTime DateAdded { get; set; }
}
}