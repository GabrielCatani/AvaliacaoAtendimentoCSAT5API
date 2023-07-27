using System;
using AvaliacaoAtendimentoCSAT5API.Models;

namespace AvaliacaoAtendimentoCSAT5API.Services
{
	public interface ICSATService
	{
        Task<List<CSAT>> GetAsync();
        Task CreateAsync(CSAT newCSAT);
    }
}

