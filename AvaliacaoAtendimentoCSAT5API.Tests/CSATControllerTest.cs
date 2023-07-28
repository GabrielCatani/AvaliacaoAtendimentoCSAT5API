using System;
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
		private Mock<ICSATService> _mockCsatService;
		private CSATController _csatController;

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
			_mockCsatService.Setup(service => service.CreateCSAT(newCSAT));

			var result = _csatController.PostCSAT(newCSAT);

			Assert.IsNotNull(result);

            var resultStatusCode = result.Result as ObjectResult;

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

			_mockCsatService.Setup(service => service.CreateCSAT(newCSAT));

			var result = _csatController.PostCSAT(newCSAT);

			Assert.IsNotNull(result);

            var resultStatusCode = result.Result as ObjectResult;

			Assert.AreEqual(400, resultStatusCode.StatusCode);
		}

		[TestMethod]
		public async Task GetCSATByIdWhenCSATExists() {

			CSAT persistedCSAT = new CSAT
            {
				Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Melhor atendimento do mundo",
                ProblemSolved = false,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

			_mockCsatService.Setup(service
				=> service.GetCSATById("67c00120-77ed-458b-aeec-e8a46e555fa3"))
				.ReturnsAsync(persistedCSAT);

			var result = await _csatController.GetCSAT(
										"67c00120-77ed-458b-aeec-e8a46e555fa3");
            Assert.IsNotNull(result);

			var okResult = result.Result as ObjectResult;

			Assert.AreEqual(200, okResult.StatusCode);
			Assert.AreEqual(persistedCSAT, okResult.Value);
        }

        [TestMethod]
        public async Task GetCSATByIdWhenCSATNotFound()
        {

            _mockCsatService.Setup(service
                => service.GetCSATById("52c00120-77ed-458b-aeec-e8a46e555f43"))
                .ReturnsAsync((CSAT)null);

            var result = await _csatController.GetCSAT(
                                        "67c00120-77ed-458b-aeec-e8a46e555fa3");
            Assert.IsNotNull(result);

			var notFoundResult = result.Result as NotFoundResult;

            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

		[TestMethod]
		public async Task GetCSATByIdWhenIdIsEmpty()
		{
			var result = await _csatController.GetCSAT(" ");

			Assert.IsNotNull(result);

			var badRequest = result.Result as BadRequestResult;

			Assert.AreEqual(400, badRequest.StatusCode);
		}
    }
}

