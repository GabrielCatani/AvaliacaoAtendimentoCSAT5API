using System;
namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class FCR
	{
        public int Total { get; set;}
		public int Positive { get; set; }
		public int Negative { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            FCR other = (FCR)obj;

            return Total == other.Total &&
                Positive == other.Positive &&
                Negative == other.Negative;
    }

        public override int GetHashCode()
        {
            return HashCode.Combine(Total, Positive, Negative);
        }
    }
}

