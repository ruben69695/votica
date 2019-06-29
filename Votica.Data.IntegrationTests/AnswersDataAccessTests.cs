using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Votica.Data.Infrastructure;
using Votica.Data.IntegrationTests.Utils;
using Votica.Domain;

namespace Votica.Data.IntegrationTests
{
    [TestFixture]
    public class AnswersDataAccessTests
    {
        private IUnitOfWork _uwo;
        private IRepositoryBase<Answer> _answerRepo;
        private IRepositoryBase<Poll> _pollRepo;
        private IRepositoryBase<Question> _questionRepo;
        private IRepositoryBase<QuestionType> _questionTypeRepo;

        
        [SetUp]
        public void Setup()
        {
            _uwo = new UnitOfWork(PrepareTestingLab.TestContext);
            _answerRepo = _uwo.GetRepository<Answer>();
            _pollRepo = _uwo.GetRepository<Poll>();
            _questionRepo = _uwo.GetRepository<Question>();
            _questionTypeRepo = _uwo.GetRepository<QuestionType>();
        }

        [Test]
        public async Task AddAsync_AddAnswerToQuestion_ShouldAddQuestions()
        {
            var poll = await DataAccessHelpers.AddPollAsync(_pollRepo, "Event 1", "Short description", DateTimeOffset.Now.AddDays(5.0));
            var qType = await DataAccessHelpers.AddQuestionTypeAsync(_questionTypeRepo, "Simple");
            var question = await DataAccessHelpers.AddQuestionAsync(_questionRepo, "Do you want to sleep?", qType, poll);
            var afirmativeAnswer = new Answer {
                Name = "Yes",
                Question = question
            };

            var result = await _answerRepo.AddAsync(afirmativeAnswer);

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async Task FindByAsync_FindItemByExpression_ShouldReturnTheCorrectItem()
        {
            var poll = await DataAccessHelpers.AddPollAsync(_pollRepo, "Event 1", "Short description", DateTimeOffset.Now.AddDays(5.0));
            var qType = await DataAccessHelpers.AddQuestionTypeAsync(_questionTypeRepo, "Simple");
            var question = await DataAccessHelpers.AddQuestionAsync(_questionRepo, "Do you want to eat?", qType, poll);
            var negativeAnswer = new Answer {
                Name = "No",
                Question = question
            };
            var answerResult = await _answerRepo.AddAsync(negativeAnswer);

            var answerFound = await _answerRepo.FindByAsync(answer => answer.Id == answerResult.Id);

            Assert.That(answerFound.Name, Is.EqualTo("No"));
        }

        [Test]
        public async Task FindWhere_FindAnswersByExpression_ShouldFindTheCorrectQuestions()
        {
            var poll = await DataAccessHelpers.AddPollAsync(_pollRepo, "Event 2", "Short description", DateTimeOffset.Now.AddDays(9.0));
            var qType = await DataAccessHelpers.AddQuestionTypeAsync(_questionTypeRepo, "Simple");
            var question = await DataAccessHelpers.AddQuestionAsync(_questionRepo, "Are you mad?", qType, poll);
            var afirmativeAnswer = new Answer {
                Name = "Yes",
                Question = question
            };
            var negativeAnswer = new Answer {
                Name = "No",
                Question = question
            };
            afirmativeAnswer = await _answerRepo.AddAsync(afirmativeAnswer);
            negativeAnswer = await _answerRepo.AddAsync(negativeAnswer);

            var answersFound = await _answerRepo.FindWhere(answer => answer.Question.Id == question.Id)
                .ToListAsync();

            Assert.That(answersFound.Count, Is.EqualTo(2));
        }
        
        [Test]
        public async Task FindByIdAsync_FindById_ShouldReturnTheCorrectItem()
        {
            var poll = await DataAccessHelpers.AddPollAsync(_pollRepo, "Event 12", "Short description", DateTimeOffset.Now.AddDays(9.0));
            var qType = await DataAccessHelpers.AddQuestionTypeAsync(_questionTypeRepo, "Simple");
            var question = await DataAccessHelpers.AddQuestionAsync(_questionRepo, "Are you serious?", qType, poll);
            var answer = new Answer {
                Name = "WTF?",
                Question = question
            };
            answer = await _answerRepo.AddAsync(answer);

            var answerFound = await _answerRepo.GetByIdAsync(answer.Id);

            Assert.That(answer, Is.Not.Null);
        }

        [Test]
        public async Task RemoveAsync_RemoveAnswerFromQuestion_ShouldRemoveIt()
        {
            var poll = await DataAccessHelpers.AddPollAsync(_pollRepo, "Event 14", "Short description", DateTimeOffset.Now.AddDays(9.0));
            var qType = await DataAccessHelpers.AddQuestionTypeAsync(_questionTypeRepo, "Simple");
            var question = await DataAccessHelpers.AddQuestionAsync(_questionRepo, "Where do you want to go?", qType, poll);
            var answer1 = new Answer {
                Name = "New York?",
                Question = question
            };
            var answer2 = new Answer {
                Name = "Chicago",
                Question = question
            };
            answer1 = await _answerRepo.AddAsync(answer1);
            answer2 = await _answerRepo.AddAsync(answer2);

            await _answerRepo.RemoveAsync(answer2, answer2.Id);
            var result = await _answerRepo.FindWhere(answer => answer.Question.Id == question.Id).ToListAsync();

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task UpdateAync_UpdateQuestion_ShouldUpdateIt()
        {
            var poll = await DataAccessHelpers.AddPollAsync(_pollRepo, "Event 24", "Short description", DateTimeOffset.Now.AddDays(9.0));
            var qType = await DataAccessHelpers.AddQuestionTypeAsync(_questionTypeRepo, "Simple");
            var question = await DataAccessHelpers.AddQuestionAsync(_questionRepo, "Where do you want to go?", qType, poll);
            var answer1 = new Answer {
                Name = "New York?",
                Question = question
            };
            answer1 = await _answerRepo.AddAsync(answer1);
            answer1.Name = "South Portland Maine";

            var result = await _answerRepo.UpdateAsync(answer1, answer1.Id);

            Assert.That(result.Name, Is.EqualTo("South Portland Maine"));
        }

    }
}