﻿using System;
using AvaliacaoAtendimentoCSAT5API.Controllers;
using AvaliacaoAtendimentoCSAT5API.Services;
using AvaliacaoAtendimentoCSAT5API.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace AvaliacaoAtendimentoCSAT5API.Tests
{
	[TestClass]
	public class CSATControllerTest
	{
		private Mock<ICSATService>? _mockCsatService;
		private CSATController? _csatController;

		[TestInitialize]
		public void TestInitialize()
		{
			_mockCsatService = new Mock<ICSATService>();
			_csatController = new CSATController(_mockCsatService.Object);
		}

		[TestMethod]
		public void CreateValidCSAT()
		{
			CSAT newCSAT = new CSAT
			{
				Score = 2,
				Comment = "Fulano foi muito educado, mas infelizmente" +
				" não consegui resolver meu problema",
				ProblemSolved = false,
				Email = "fulano@fulano.com.br",
				TimeStamp = new DateTime()
			};

			Guid expectedGuid = Guid.NewGuid();

			newCSAT.Id = expectedGuid;
			_mockCsatService.Setup(service => service.CreateAsync(newCSAT));

			var result = _csatController.PostCSAT(newCSAT);
			var resultStatusCode = result.Result as ObjectResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(200, resultStatusCode.StatusCode);
		}

		[TestMethod]
		public void CreateInvalidCSATWithBelowOneScore()
		{
			CSAT newCSAT = new CSAT {
				Score = -1,
				Comment = "Péssimo Atendimento",
				ProblemSolved = false,
				Email = "bravo@bravo.com.br",
				TimeStamp = new DateTime()
			};

			_mockCsatService.Setup(service => service.CreateAsync(newCSAT));

			var result = _csatController.PostCSAT(newCSAT);
			var resultStatusCode = result.Result as ObjectResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(400, resultStatusCode.StatusCode);
		}
	}
}
