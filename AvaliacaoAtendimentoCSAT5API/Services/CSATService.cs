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
		public async Task CreateCSAT(CSAT newCSAT) =>
			await _csatCollection.InsertOneAsync(newCSAT);

		//Get CSAT by id
		public async Task<CSAT?> GetCSATById(string id)
		{
			Guid parsedGuid;
			Guid.TryParse(id, out parsedGuid);
			var filter = Builders<CSAT>.Filter.Eq("_id", parsedGuid);

            return await _csatCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<CSAT>> ListAllCSAT(string score,
												  string fcr,
												  string email)
        {
			var builder = Builders<CSAT>.Filter;
			FilterDefinition<CSAT> filter = builder.Empty;
			Boolean parsedFCR;
			Boolean.TryParse(fcr, out parsedFCR);


			if (!string.IsNullOrEmpty(score))
			{
				filter = filter & builder.Eq(obj => obj.Score, int.Parse(score));
			}

			if (!string.IsNullOrEmpty(fcr))
			{
				filter = filter & builder.Eq(obj =>
											 obj.ProblemSolved, parsedFCR);
			}

			if (!string.IsNullOrEmpty(email))
			{
				filter = filter & builder.Eq(obj => obj.Email, email);
			}

			return await _csatCollection.Find(filter).ToListAsync();
        }

        public async Task UpdateProblemSolved(string id, CSAT updatedCSAT) =>
			await _csatCollection.ReplaceOneAsync(csat =>
												  csat.Id == Guid.Parse(id),
																updatedCSAT);

        public async Task UpdateComment(string id, CSAT updatedCSAT) =>
			await _csatCollection.ReplaceOneAsync(csat =>
                                                  csat.Id == Guid.Parse(id),
                                                                updatedCSAT);

        public async Task<CSATSummaryByEmail?> FormSummary(List<CSAT> filteredCSATs)
        {
			
			CSATSummaryByEmail csatSummary = new CSATSummaryByEmail();

			var totalPromo = filteredCSATs.Where(csat =>
											csat.Score == 5).Count();

			if (filteredCSATs.Count() == 0)
			{
				csatSummary.Score = 0;
			}
			else
			{
                csatSummary.Score = totalPromo / filteredCSATs.Count();
            }

			FCR fcr = new FCR
			{
				Positive = filteredCSATs.Where(csat =>
                                            csat.ProblemSolved == true).Count(),
				Negative = filteredCSATs.Where(csat =>
                                            csat.ProblemSolved == false).Count(),
				Total = filteredCSATs.Where(csat =>
                                            csat.ProblemSolved == true).Count() +
                        filteredCSATs.Where(csat =>
                                            csat.ProblemSolved == false).Count(),

            };

			csatSummary.Fcr = fcr;

            return csatSummary;
        }
    }
}

