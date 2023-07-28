using System;
namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class CSATSummaryByEmail
	{
		public decimal Score { get; set; }
		public int TotalFCR { get; set; }
		public int PositiveFCRCount { get; set; }
		public int NegativeFCRCount { get; set; }

        public override string ToString()
        {
            return "{\n\tscore: " + Score
                    + "\n\tfcr: {"
                    + "\n\t\ttotal: " + TotalFCR
                    + "\n\t\tpositives: " + PositiveFCRCount
                    + "\n\t\tnegatives: " + NegativeFCRCount
                    + "\n}\n"
                    + "\n}";
        }
    }
}

