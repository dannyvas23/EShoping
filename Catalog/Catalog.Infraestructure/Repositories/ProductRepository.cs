﻿using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infraestructure.Data;
using MongoDB.Driver;

namespace Catalog.Infraestructure.Repositories;

public class ProductRepository: IProductRepository, IBrandRepository, ITypeRepository
{
    private readonly ICatalogContextSeed _context;

    public ProductRepository(ICatalogContextSeed context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _context
            .Products
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _context
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();

    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByBrand(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Brands.Name, name);
        return await _context
            .Products
            .Find(filter)
            .ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _context
            .Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
        
        DeleteResult deleteResult = await _context
            .Products
            .DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;

    }

    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _context
            .Brands
            .Find(b => true)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _context
            .Types
            .Find(t => true)
            .ToListAsync();
    }
}