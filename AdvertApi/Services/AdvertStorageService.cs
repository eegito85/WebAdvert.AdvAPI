using AdvertApi.Models.Models;
using AdvertApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AdvertApi.DataModels;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace AdvertApi.Services
{
    public class AdvertStorageService : IAdvertStorageService
    {
        private readonly IMapper _mapper;

        public AdvertStorageService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<string> Add(AdvertModel advertModel)
        {
            var dbModel = _mapper.Map<AdvertDbModel>(advertModel);

            dbModel.Id = new Guid().ToString();
            dbModel.CreationDateTime = DateTime.UtcNow;
            dbModel.Status = Models.Enums.AdvertStatus.Pending;

            using (var client = new AmazonDynamoDBClient())
            {
                using(var context = new DynamoDBContext(client))
                {
                    await context.SaveAsync(dbModel);
                }
            }
            return dbModel.Id;
        }

        public async Task Confirm(ConfirmAdvertModel confirmModel)
        {
            using (var client = new AmazonDynamoDBClient())
            {
                using (var context = new DynamoDBContext(client))
                {
                    var record = await context.LoadAsync<AdvertDbModel>(confirmModel.Id);
                    if (record == null)
                    {
                        throw new KeyNotFoundException($"O registro com Id = {confirmModel.Id} não foi encontrado");
                    }
                    if(confirmModel.Status == Models.Enums.AdvertStatus.Active)
                    {
                        record.Status = Models.Enums.AdvertStatus.Active;
                        await context.SaveAsync(record);
                    }
                    else
                    {
                        await context.DeleteAsync(record);
                    }
                }
            }
        }
    }
}
