using System;
using AvaliacaoAtendimentoCSAT5API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace AvaliacaoAtendimentoCSAT5API.Services
{
	public class CSATService
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
		}

		//Get All CSATs
		public async Task<List<CSAT>> GetAsync() =>
			await _csatCollection.Find(_ => true).ToListAsync();

		//Insert new CSATs
		public async Task CreateAsync(CSAT newCSAT) =>
			await _csatCollection.InsertOneAsync(newCSAT);
	}
}

