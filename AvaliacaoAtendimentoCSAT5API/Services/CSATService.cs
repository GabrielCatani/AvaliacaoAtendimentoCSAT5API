using System;
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
		public async Task CreateCSAT(CSAT newCSAT) =>
			await _csatCollection.InsertOneAsync(newCSAT);

		//Get CSAT by id
		public async Task<CSAT?> GetCSATById(string id)
		{
			var filter = Builders<CSAT>.Filter.Eq("_id", Guid.Parse(id));

            return await _csatCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<CSAT>> ListAllCSAT(string score,
												  string fcr,
												  string email)
        {
			var builder = Builders<CSAT>.Filter;
			FilterDefinition<CSAT> filter = builder.Empty;

			if (!string.IsNullOrEmpty(score))
			{
				filter = filter & builder.Eq(obj => obj.Score, int.Parse(score));
			}

			if (!string.IsNullOrEmpty(fcr))
			{
				filter = filter & builder.Eq(obj =>
											 obj.ProblemSolved, bool.Parse(fcr));
			}

			if (!string.IsNullOrEmpty(email))
			{
				filter = filter & builder.Eq(obj => obj.Email, email);
			}

			return await _csatCollection.Find(filter).ToListAsync();
        }

		public async Task UpdateFCR(string id, CSAT updatedCSAT) =>
			await _csatCollection.ReplaceOneAsync(csat =>
												  csat.Id == Guid.Parse(id),
																updatedCSAT);
    }
}

