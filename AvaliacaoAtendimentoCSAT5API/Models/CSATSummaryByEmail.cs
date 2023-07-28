using System;
namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class CSATSummaryByEmail
	{
		public decimal Score { get; set; }
		public int TotalFCR { get; set; }
		public int PositiveFCRCount { get; set; }
		public int NegativeFCRCount { get; set; }
	}
}

