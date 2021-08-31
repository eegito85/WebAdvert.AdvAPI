﻿using AdvertApi.Models.Enums;
using Amazon.DynamoDBv2.DataModel;
using System;

namespace AdvertApi.DataModels
{
    [DynamoDBTable("Adverts")]
    public class AdvertDbModel
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string Title { get; set; }
        
        [DynamoDBProperty]
        public string Description { get; set; }
        
        [DynamoDBProperty]
        public double Price { get; set; }
        
        [DynamoDBProperty]
        public DateTime CreationDateTime { get; set; }
        
        [DynamoDBProperty]
        public AdvertStatus Status { get; set; }
    }
}
