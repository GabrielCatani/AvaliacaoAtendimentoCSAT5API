using System;
using AvaliacaoAtendimentoCSAT5API.Models;

namespace AvaliacaoAtendimentoCSAT5API.Services
{
	public interface ICSATService
	{
        Task CreateAsync(CSAT newCSAT);
        Task<CSAT?> GetCSATById(Guid _id);
    }
}

