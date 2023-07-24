using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AvaliacaoAtendimentoCSAT5API.Models
{
	public class CSAT
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string? Id { get; set; }

		[BsonElement("score")]
		public int Score { get; set; }

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
				    + "\n\tComment: " + Comment
					+ "\n\tFCR: " + ProblemSolved
					+ "\n\tEmail: " + Email
					+ "\n\tTimeStamp: " + TimeStamp
					+ "\n}";
        }
    }
}

