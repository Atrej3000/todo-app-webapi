using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TodoAPI.Repositories;
using TodoAPI.Services;
using Moq;
using TodoAPI.Models;
using System.Threading.Tasks;
using TodoAPI.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DTO;
using System.Linq;
using TodoAPI.Mapping;

namespace TodoAPI.Tests
{
    [TestFixture]
    public class CardControllerTests
    {
        private Mock<CardRepository> _repo;
        private CardContext _context;
        private Mock<ICardService> _service;
        private Mock<IMapper> _mapper;
        private CardsController _cardsController;
        private Card _card;
        private CardDTO _cardDTO;
        private List<Card> _cardList;
        private List<CardDTO> _cardDTOList;
        public CardControllerTests()
        {
            _repo = new Mock<CardRepository>(_context);
            _service = new Mock<ICardService>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<Card, CardDTO>(It.IsAny<Card>())).Returns(new CardDTO());
            _cardsController = new CardsController(_service.Object, _mapper.Object);
            _card = new Card { Id = 1, Text = "Todo", Complete = true };
            _cardDTO = new CardDTO { Id = _card.Id, Text = _card.Text, Complete = _card.Complete };
            _cardList = new List<Card> {
                new Card { Id = 1, Text = "Todo", Complete = true },
                new Card { Id = 2, Text = "Todo2", Complete = true },
                new Card { Id = 3, Text = "Todo3", Complete = true }
            };
            _cardDTOList = new List<CardDTO> {
                new CardDTO { Id = 1, Text = "Todo", Complete = true },
                new CardDTO { Id = 2, Text = "Todo2", Complete = true },
                new CardDTO { Id = 3, Text = "Todo3", Complete = true }
            };
        }

        [Test]
        public async Task GetCards_WhenCalled_ReturnOk()
        {
            _service.Setup(s => s.GetAllAsync().Result).Returns(_cardList);
            //_mapper.Setup(m => m.Map<IEnumerable<Card>, IEnumerable<CardDTO>>(It.IsAny<IEnumerable<Card>>())).Returns(_cardDTOList);
            _cardsController = new CardsController(_service.Object, _mapper.Object);
            var result = await _cardsController.GetCards();
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        public async Task GetCards_WhenCalled_VerifyServiceGetAll()
        {
            await _cardsController.GetCards();
            _service.Verify(s => s.GetAllAsync());
        }
        [Test]
        public async Task GetCards_WhenCalled_EmptyListOfDTO()
        {
            _service.Setup(s => s.GetAllAsync().Result).Returns(new List<Card>());
            //_mapper.Setup(m => m.Map<IEnumerable<Card>, IEnumerable<CardDTO>>(It.IsAny<IEnumerable<Card>>())).Returns(_cardDTOList);
            _cardsController = new CardsController(_service.Object, _mapper.Object);
            var result = await _cardsController.GetCards();
            var actual = (result.Result as OkObjectResult).Value as IEnumerable<CardDTO>;
            Assert.That(actual, Is.Empty);
        }

        [Test]
        [TestCase(1)]
        public async Task GetCard_ProductNull_ReturnsNotFound(int id)
        {
            _service.Setup(s => s.GetOneAsync(id)).Returns(Task.FromResult<Card>(null));
            var result = await _cardsController.GetCard(id);
            Assert.That(result.Result, Is.TypeOf<NotFoundResult>());
        }
        [Test]
        [TestCase(1)]
        public async Task GetCard_ValidCard_ReturnsNotFound(int id)
        {
            _service.Setup(s => s.GetOneAsync(id)).Returns(Task.FromResult(new Card()));
            var result = await _cardsController.GetCard(id);
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }
        [Test]
        [TestCase(1)]
        public async Task GetCards_WhenCalled_VerifyServiceGetOne(int id)
        {
            await _cardsController.GetCard(id);
            _service.Verify(s => s.GetOneAsync(id));
        }
    }
}
