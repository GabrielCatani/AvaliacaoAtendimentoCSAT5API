﻿using System;
using AvaliacaoAtendimentoCSAT5API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace AvaliacaoAtendimentoCSAT5API.Services
{
	public class CSATService : ICSATService
	{
		private readonly IMongoCollection<CSAT> _csatCollection;

		public CSATService(IOptions<AvaliacaoCSATDatabaseSettings>
			avaliacaoCSATDatabaseSettings)
		{
			var mongoClient = new MongoClient(
				avaliacaoCSATDatabaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				avaliacaoCSATDatabaseSettings.Value.DatabaseName);

			_csatCollection = mongoDatabase.GetCollection<CSAT>(
				avaliacaoCSATDatabaseSettings.Value.CSATCollectionName);

			var emailIndex = new CreateIndexModel<CSAT>(Builders<CSAT>
									.IndexKeys.Ascending("attendantEmail"));

			var email_createdAtIndex = new CreateIndexModel<CSAT>(Builders<CSAT>
									.IndexKeys.Ascending("attendantEmail")
											  .Ascending("createdAt"));

			_csatCollection.Indexes.CreateMany(new[] {emailIndex
													  , email_createdAtIndex});

		}

		//Insert new CSATs
		public async Task CreateAsync(CSAT newCSAT) =>
			await _csatCollection.InsertOneAsync(newCSAT);

		//Get CSAT by id
		public async Task<CSAT?> GetCSATById(string id)
		{
			var filter = Builders<CSAT>.Filter.Eq("_id", Guid.Parse(id));

            return await _csatCollection.Find(filter).FirstOrDefaultAsync();
        }
			
    }
}

