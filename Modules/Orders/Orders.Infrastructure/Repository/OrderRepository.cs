using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Orders.Domain.Interfaces;
using Orders.Domain.Models;
using Orders.Infrastructure.Entities;
using restatunt.Shared.Constans;

namespace Orders.Infrastructure.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<OrderEntity> _collection;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public OrderRepository(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
        var client = new MongoClient(_configuration
            .GetValue<string>(ConstantsServer.CONNECTION_STRING));
        _mongoDatabase = client.GetDatabase(_configuration
            .GetValue<string>(ConstantsServer.DATABASE_NAME));
        _collection = _mongoDatabase.GetCollection<OrderEntity>(_configuration
            .GetValue<string>(ConstantsServer.COLLECTION_ORDERS));
    }


    public async Task<OrderModel> CreateAsync(OrderModel order)
    {
        var entity = _mapper.Map<OrderEntity>(order);
        await _collection.InsertOneAsync(entity);
        return _mapper.Map<OrderModel>(entity);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(e => e.Id == id);
        return true;
    }

    public async Task<List<OrderModel>> GetAllAsync()
    {
        var ListEntity = await _collection.Find(e => true)
            .ToListAsync()
            .ConfigureAwait(false);
        return _mapper.Map<List<OrderModel>>(ListEntity);
    }

    public async Task<OrderModel> GetByIdAsync(string id)
    {
        var Entity = await _collection.Find(e => e.Id == id)
            .FirstOrDefaultAsync()
            .ConfigureAwait(false);
        return _mapper.Map<OrderModel>(Entity);
    }

    public async Task<OrderModel> UpdateAsync(OrderModel order)
    {
        var entity = _mapper.Map<OrderEntity>(order);
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return order;
    }

}
