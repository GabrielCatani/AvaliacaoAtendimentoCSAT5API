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

		[TestMethod]
		public async Task ListAllCSATsPassingNoParameters()
		{
			var csats = new List<CSAT>
			{
				new CSAT { Id = new Guid(),
						   Score = 3,
					       Comment = "bora", ProblemSolved = true,
						   Email = "eu@voce.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 2,
                           Comment = "Muito ruim", ProblemSolved = false,
                           Email = "eu@voce.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 2,
                           Comment = "péssimo", ProblemSolved = false,
                           Email = "eu@voce.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 3,
                           Comment = "Beleza", ProblemSolved = true,
                           Email = "fera@fera.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 5,
                           Comment = "Bom demaise", ProblemSolved = true,
                           Email = "fera@fera.com.br", TimeStamp = new DateTime()},
            };

			List<CSAT> score2List = csats.GetRange(1, 2);

			_mockCsatService.Setup(service
						=> service.ListAllCSAT("2", "true", "eu@voce.com.br"))
						.ReturnsAsync(score2List);

			var result = await _csatController.GetAllCSAT("2",
													"true",
													"eu@voce.com.br");

			Assert.IsNotNull(result);
			var okResult = result.Result as ObjectResult;

			List<CSAT> returnedList = (List<CSAT>)okResult.Value;

			Assert.AreEqual(200, okResult.StatusCode);
			Assert.IsTrue(score2List.SequenceEqual(returnedList));
		}

        [TestMethod]
        public async Task ListAllCSATsPassingScoreFCREmailParams()
        {
            var csats = new List<CSAT>
            {
                new CSAT { Id = new Guid(),
                           Score = 3,
                           Comment = "bora", ProblemSolved = true,
                           Email = "eu@voce.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 2,
                           Comment = "Muito ruim", ProblemSolved = false,
                           Email = "eu@voce.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 2,
                           Comment = "péssimo", ProblemSolved = false,
                           Email = "eu@voce.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 3,
                           Comment = "Beleza", ProblemSolved = true,
                           Email = "fera@fera.com.br", TimeStamp = new DateTime()},
                new CSAT { Id = new Guid(),
                           Score = 5,
                           Comment = "Bom demaise", ProblemSolved = true,
                           Email = "fera@fera.com.br", TimeStamp = new DateTime()},
            };

            _mockCsatService.Setup(service
                        => service.ListAllCSAT("", "", ""))
                        .ReturnsAsync(csats);

            var result = await _csatController.GetAllCSAT();

            Assert.IsNotNull(result);
            var okResult = result.Result as ObjectResult;

            List<CSAT> returnedList = (List<CSAT>)okResult.Value;

            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsTrue(csats.SequenceEqual(returnedList));

        }

		[TestMethod]
		public async Task UpdateFCRofValidCSAT()
		{
            CSAT persistedCSAT = new CSAT
            {
                Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Melhor atendimento do mundo",
                ProblemSolved = false,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

            CSAT updatedCSAT = new CSAT
            {
                Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Melhor atendimento do mundo",
                ProblemSolved = true,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

            _mockCsatService.Setup(srv
                => srv.GetCSATById("67c00120-77ed-458b-aeec-e8a46e555fa3"))
                                    .ReturnsAsync(persistedCSAT);
                        
            _mockCsatService.Setup(srv
                => srv.UpdateProblemSolved(
                                     "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                     updatedCSAT)).Returns(Task.CompletedTask);

            var result = _csatController.UpdateFCR(
                                         "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                         "true");

            Assert.IsNotNull(result);

            var noContent = result.Result as NoContentResult;

            Assert.AreEqual(204, noContent.StatusCode);
        }

        [TestMethod]
        public async Task UpdateFCRNotFoundCSAT()
        {
            CSAT updatedCSAT = new CSAT
            {
                Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Melhor atendimento do mundo",
                ProblemSolved = true,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

            _mockCsatService.Setup(srv
                => srv.UpdateProblemSolved(
                                     "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                     updatedCSAT)).Returns(Task.CompletedTask);

            var result = _csatController.UpdateFCR(
                                         "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                         "true");

            Assert.IsNotNull(result);

            var notFound = result.Result as NotFoundResult;

            Assert.AreEqual(404, notFound.StatusCode);
        }

        [TestMethod]
        public async Task UpdateCommentofValidCSAT()
        {
            CSAT persistedCSAT = new CSAT
            {
                Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Melhor atendimento do mundo",
                ProblemSolved = false,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

            CSAT updatedCSAT = new CSAT
            {
                Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Atendimento F***",
                ProblemSolved = false,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

            _mockCsatService.Setup(srv
                => srv.GetCSATById("67c00120-77ed-458b-aeec-e8a46e555fa3"))
                                    .ReturnsAsync(persistedCSAT);

            _mockCsatService.Setup(srv
                => srv.UpdateProblemSolved(
                                     "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                     updatedCSAT)).Returns(Task.CompletedTask);

            var result = _csatController.UpdateComment(
                                         "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                         "Atendimento F***");

            Assert.IsNotNull(result);

            var noContent = result.Result as NoContentResult;

            Assert.AreEqual(204, noContent.StatusCode);
        }

        [TestMethod]
        public async Task UpdateCommentNotFoundCSAT()
        {
            CSAT updatedCSAT = new CSAT
            {
                Id = new Guid("67c00120-77ed-458b-aeec-e8a46e555fa3"),
                Score = 5,
                Comment = "Melhor atendimento do mundo",
                ProblemSolved = true,
                Email = "otimo@otimo.com.br",
                TimeStamp = new DateTime()
            };

            _mockCsatService.Setup(srv
                => srv.UpdateProblemSolved(
                                     "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                     updatedCSAT)).Returns(Task.CompletedTask);

            var result = _csatController.UpdateComment(
                                         "67c00120-77ed-458b-aeec-e8a46e555fa3",
                                         "Atendimento F***");

            Assert.IsNotNull(result);

            var notFound = result.Result as NotFoundResult;

            Assert.AreEqual(404, notFound.StatusCode);
        }
    }
}

