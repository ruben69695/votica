using System;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Votica.Data.Infrastructure;
using Votica.Domain;
using System.Threading.Tasks;

namespace Votica.Data.IntegrationTests
{
    [TestFixture]
    public class PollDataAcessTests
    {
        private UnitOfWork _uow;
        private IRepositoryBase<Poll> _repo;

        [SetUp]
        public void Setup()
        {
            _uow = new UnitOfWork(PrepareTestingLab.TestContext);
            _repo = _uow.GetRepository<Poll>();
        }

        [Test]
        public async Task AddAsync_AddNewPoll_ShouldInsertIntoDatabase()
        {
            var poll = new Poll
            {
                Name = "Summer event",
                Description = "This is a short description for the summer event",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now.AddDays(5.0)
            };

            var pollResult = await _repo.AddAsync(poll);

            Assert.That(pollResult.Name, Is.EqualTo(poll.Name));
        }

        [Test]
        public async Task FindByAsync_FindItemByExpression_ShouldReturnTheCorrectItem()
        {
            var poll = new Poll
            {
                Name = "Mega Freak day event",
                Description = "This is a short description",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now.AddDays(5)
            };
            await _repo.AddAsync(poll);

            var pollResult = await _repo.FindByAsync(p => p.Name == "Mega Freak day event");

            Assert.That(pollResult.Name, Is.EqualTo(poll.Name));
        }
        [Test]
        public async Task FindWhere_FindItemByExpression_ShouldReturnTheCorrectItem()
        {
            var pollItem = new Poll
            {
                Name = "Day 24th event",
                Description = "This is a short 1 description",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now.AddDays(5)
            };

            var pollItem2 = new Poll
            {
                Name = "Day event",
                Description = "This is a short 1 description",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now
            };
            await _repo.AddAsync(pollItem);
            await _repo.AddAsync(pollItem2);

            var pollResult = await _repo.FindWhere(p => p.Description == "This is a short 1 description").ToListAsync();

            Assert.That(pollResult.Count, Is.GreaterThan(1));
        }

        [Test]
        public async Task GetByIdAsync_FindById_ShouldReturnTheCorrectItem()
        {
            var pollItem = new Poll
            {
                Name = "Day event",
                Description = "This is a short 2 description",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now.AddDays(8)
            };
            var pollInserted = await _repo.AddAsync(pollItem);

            var pollResult = await _repo.GetByIdAsync(pollInserted.Id);

            Assert.That(pollInserted.Id, Is.EqualTo(pollResult.Id));
        }

        [Test]
        public async Task RemoveAsync_RemovePollItem_ShouldDeleteIt()
        {
            var pollItem = new Poll
            {
                Name = "Day event",
                Description = "This is a short 2 description",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now.AddDays(8)
            };
            var pollInserted = await _repo.AddAsync(pollItem);
            await _repo.RemoveAsync(pollInserted, pollInserted.Id);
            var pollResult = await _repo.GetByIdAsync(pollInserted.Id);

            Assert.That(pollResult, Is.Null);
        }

        [Test]
        public async Task UpdateAsync_UpdateThePoll_ShouldUpdateThePoll()
        {
            var pollItem = new Poll
            {
                Name = "Spring End Event",
                Description = "This is a short description",
                CreationDate = DateTimeOffset.Now,
                ExpirationDate = DateTimeOffset.Now.AddDays(8)
            };
            var pollInserted = await _repo.AddAsync(pollItem);
            pollInserted.Description = "This is a null description";

            var pollResult = await _repo.UpdateAsync(pollInserted, pollInserted.Id);

            Assert.That(pollResult.Description, Is.EqualTo("This is a null description"));
        }

    }
}