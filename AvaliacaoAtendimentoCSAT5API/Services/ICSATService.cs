using System;
using AvaliacaoAtendimentoCSAT5API.Models;

namespace AvaliacaoAtendimentoCSAT5API.Services
{
	public interface ICSATService
	{
        Task CreateCSAT(CSAT newCSAT);
        Task<CSAT?> GetCSATById(string id);
        Task<List<CSAT>> ListAllCSAT(string score, string fcr, string email);
        Task UpdateFCR(string id, CSAT updatedCSAT);
    }
}

