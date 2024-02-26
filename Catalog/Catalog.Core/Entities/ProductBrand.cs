using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities;

public class ProductBrand
{
    [BsonElement("Name")] 
    public string Name { get; set; }
}