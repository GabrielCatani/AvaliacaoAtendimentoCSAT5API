using System;
namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class CSAT
	{
	
		public Guid Id { get; set; }
		public int Score { get; set; }
		public string? Comment { get; set; }
		public bool problemSolved { get; set; }
		public string? Email { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}

