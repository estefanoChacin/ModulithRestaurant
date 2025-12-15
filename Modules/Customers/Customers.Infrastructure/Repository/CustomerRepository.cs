using AutoMapper;
using Customers.Domain.Interfaces;
using Customers.Domain.Models;
using Customers.Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using restatunt.Shared.Constans;

namespace Customers.Infrastructure.Repository;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoDatabase _mongoDatabase;
    private readonly IMongoCollection<CustomerEntity> _collection;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public CustomerRepository(IConfiguration configuration, IMapper mapper)
    {
        _mapper = mapper;
        _configuration = configuration;
        var client = new MongoClient(_configuration.GetValue<string>(ConstantsServer.CONNECTION_STRING));
        _mongoDatabase = client.GetDatabase(_configuration.GetValue<string>(ConstantsServer.DATABASE_NAME));
        _collection = _mongoDatabase.GetCollection<CustomerEntity>(_configuration.GetValue<string>(ConstantsServer.COLLECTION_CUSTOMERS));
    }


    public async Task<CustomerModel> CreateAsync(CustomerModel customer)
    {
        var entity = _mapper.Map<CustomerEntity>(customer);
        await _collection.InsertOneAsync(entity).ConfigureAwait(false);
        return _mapper.Map<CustomerModel>(entity);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(e => e.Id == id);
        return true;
    }

    public async Task<List<CustomerModel>> GetAllAsync()
    {
        var ListEntity = await _collection.Find(e => true).ToListAsync().ConfigureAwait(false);
        return _mapper.Map<List<CustomerModel>>(ListEntity);
    }


    public async Task<CustomerModel> GetByIdAsync(string id)
    {
        var ListEntity = await _collection.Find(e => e.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        return _mapper.Map<CustomerModel>(ListEntity);
    }

    public async Task<CustomerModel> UpdateAsync(CustomerModel customer)
    {
        var entity = _mapper.Map<CustomerEntity>(customer);
        await _collection.ReplaceOneAsync(x => x.Id == entity.Id, entity);
        return customer;
    }

}
