using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class CSAT
	{

		private int _score;

		[BsonId]
		[BsonGuidRepresentation(GuidRepresentation.Standard)]
		public Guid Id { get; set; }

		[BsonElement("score")]
		public int Score {
			get { return _score; }
			set { if (value >= 1 && value <= 5) {
					_score = value;
				}
			}
		}

		[BsonElement("comment")]
		public string? Comment { get; set; }

		[BsonElement("problemSolved")]
		public bool ProblemSolved { get; set; }

		[BsonElement("attendantEmail")]
		public string? Email { get; set; }

		[BsonElement("createdAt")]
		[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
		public DateTime TimeStamp { get; set; }

        public override string ToString()
        {
			return "{\n\tId: " + Id
					+ "\n\tScore: " + _score 
				    + "\n\tComment: " + Comment
					+ "\n\tFCR: " + ProblemSolved
					+ "\n\tEmail: " + Email
					+ "\n\tTimeStamp: " + TimeStamp
					+ "\n}";
        }

        public override bool Equals(object? obj)
        {
			if (obj == null || this.GetType() != obj.GetType())
			{
				return false;
			}

			CSAT other = (CSAT)obj;
			
            return Id == other.Id &&
				   Score == other.Score &&
				   Comment == other.Comment &&
				   ProblemSolved == other.ProblemSolved &&
				   Email == other.Email &&
				   TimeStamp == other.TimeStamp;
        }

        public override int GetHashCode()
        {
			return HashCode.Combine(Id,
									Score,
									Comment,
									ProblemSolved,
									Email,
									TimeStamp);
        }
    }
}

