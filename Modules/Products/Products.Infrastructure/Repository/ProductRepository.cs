using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using Products.Domain.Interfaces;
using Products.Domain.Models;
using Products.Infrastructure.Entities;
using restatunt.Shared.Constans;

namespace Products.Infrastructure.Repository;

public class ProductRepository : IProductRepository
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<ProductEntity> _collection;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public ProductRepository(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
        var client = new MongoClient(
            _configuration.GetValue<string>(ConstantsServer.CONNECTION_STRING));
        _mongoDatabase = client.GetDatabase(
            _configuration.GetValue<string>(ConstantsServer.DATABASE_NAME));
        _collection = _mongoDatabase.GetCollection<ProductEntity>(
            _configuration.GetValue<string>(ConstantsServer.COLLECTION_PRODUCTS));
    }

    public async Task<ProductModel> CreateAsync(ProductModel product)
    {
        var entity = _mapper.Map<ProductEntity>(product);
        await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        return _mapper.Map<ProductModel>(entity);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(e => e.Id == id);
        return true;
    }

    public async Task<List<ProductModel>> GetAllAsync()
    {
        var ListEntity = await _collection.Find(e => true)
            .ToListAsync()
            .ConfigureAwait(false);
        return _mapper.Map<List<ProductModel>>(ListEntity);
    }

    public async Task<List<ProductModel>> GetProductsByIdsAsync(List<string> ids)
    {
        var objectIds = ids.Select(id => new ObjectId(id)).ToList();
        var filter = Builders<ProductEntity>.Filter.In("_id", objectIds);
        var ListEntity = await _collection.Find(filter)
            .ToListAsync()
            .ConfigureAwait(false);

        return _mapper.Map<List<ProductModel>>(ListEntity);
    }


    public async Task<ProductModel> GetByIdAsync(string id)
    {
        var ListEntity = await _collection.Find(e => e.Id == id)
            .ToListAsync()
            .ConfigureAwait(false);
        return _mapper.Map<ProductModel>(ListEntity);
    }

    public async Task<ProductModel> UpdateAsync(ProductModel product)
    {
        var entity = _mapper.Map<ProductEntity>(product);
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return product;
    }

}
