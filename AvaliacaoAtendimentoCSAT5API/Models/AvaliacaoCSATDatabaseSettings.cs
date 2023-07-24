using System;

namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class AvaliacaoCSATDatabaseSettings
	{
		public string ConnectionString { get; set; } = null!;
		public string DatabaseName { get; set; } = null!;
		public string CSATCollectionName { get; set; } = null!;
	}
}

