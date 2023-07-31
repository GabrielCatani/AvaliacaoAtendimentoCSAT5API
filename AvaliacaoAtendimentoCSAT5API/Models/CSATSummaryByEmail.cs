using System;
namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class CSATSummaryByEmail
	{
		public decimal Score { get; set; }
        public FCR Fcr { get; set; }

        public override string ToString()
        {
            return "{\n\tscore: " + Score
                    + "\n\tfcr: {"
                    + "\n\t\ttotal: " + Fcr.Total
                    + "\n\t\tpositives: " + Fcr.Positive
                    + "\n\t\tnegatives: " + Fcr.Negative
                    + "\n}\n"
                    + "\n}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            CSATSummaryByEmail other = (CSATSummaryByEmail)obj;

            return Score == other.Score &&
                Fcr == other.Fcr;
    }

        public override int GetHashCode()
        {
            return HashCode.Combine(Score,Fcr);
        }
    }
}

