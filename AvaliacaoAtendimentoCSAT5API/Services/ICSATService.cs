using System;
using AvaliacaoAtendimentoCSAT5API.Models;

namespace AvaliacaoAtendimentoCSAT5API.Services
{
	public interface ICSATService
	{
        Task CreateCSAT(CSAT newCSAT);
        Task<CSAT?> GetCSATById(string id);
    }
}

