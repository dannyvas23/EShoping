﻿using Catalog.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Catalog.Infraestructure.Data;

public class CatalogContextSeed: ICatalogContextSeed
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> Brands { get; }
    public IMongoCollection<ProductType> Types { get; }

    public CatalogContextSeed(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

        Brands = database.GetCollection<ProductBrand>(
            configuration.GetValue<string>("DatabaseSettings:BrandsCollection"));
        Types = database.GetCollection<ProductType>(
            configuration.GetValue<string>("DatabaseSettings:TypesCollection"));
        Products = database.GetCollection<Product>(
            configuration.GetValue<string>("DatabaseSettings:ProductCollection"));
        
        BrandContextSeed.SeedData(Brands);
        TypeContextSeed.SeedData(Types);
        ProductContextSeed.SeedData(Products);
    }
    
}