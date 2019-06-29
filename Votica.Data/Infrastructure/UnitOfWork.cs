using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Votica.Database.Generics;
using Votica.Domain;

namespace Votica.Data.Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {

        #region Variables

        private IDictionary<Type, object> _repoDictionary;

        #endregion

        #region Members

        private readonly IDatabaseContext _ctx;

        #endregion

        #region Ctor

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="ctx">DbContext object</param>
        public UnitOfWork(IDatabaseContext ctx)
        {
            _ctx = ctx;
            SetRepositories();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sync commit to the context
        /// </summary>
        public void Commit()
        {
            _ctx.Commit();
        }

        /// <summary>
        /// Async commit to the context
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            return await _ctx.CommitAsync();
        }

        /// <summary>
        /// Gets the repository based on a Domain Entity
        /// </summary>
        /// <typeparam name="TEntity">Domain Entity</typeparam>
        /// <returns></returns>
        public IRepositoryBase<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repoDictionary.ContainsKey(typeof(TEntity)))
                return _repoDictionary[typeof(TEntity)] as IRepositoryBase<TEntity>;

            _repoDictionary.Add(typeof(TEntity), new RepositoryBase<TEntity>(_ctx));

            return _repoDictionary[typeof(TEntity)] as IRepositoryBase<TEntity>;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the dictionary for all repositories in the app
        /// </summary>
        private void SetRepositories()
        {
            if (_repoDictionary == null)
                _repoDictionary = new Dictionary<Type, object>()
                {
                    { typeof(Answer), new RepositoryBase<Answer>(_ctx) },
                    { typeof(User), new RepositoryBase<User>(_ctx) },
                    { typeof(UserAnswer), new RepositoryBase<UserAnswer>(_ctx) },
                    { typeof(Poll), new RepositoryBase<Poll>(_ctx) },
                    { typeof(Question), new RepositoryBase<Question>(_ctx) },
                    { typeof(QuestionType), new RepositoryBase<QuestionType>(_ctx) }
                };

            if (!_repoDictionary.ContainsKey(typeof(Answer))) _repoDictionary.Add(typeof(Answer), new RepositoryBase<Answer>(_ctx));
            if (!_repoDictionary.ContainsKey(typeof(User))) _repoDictionary.Add(typeof(User), new RepositoryBase<User>(_ctx));
            if (!_repoDictionary.ContainsKey(typeof(UserAnswer))) _repoDictionary.Add(typeof(UserAnswer), new RepositoryBase<UserAnswer>(_ctx));
            if (!_repoDictionary.ContainsKey(typeof(Poll))) _repoDictionary.Add(typeof(Poll), new RepositoryBase<Poll>(_ctx));
            if (!_repoDictionary.ContainsKey(typeof(Question))) _repoDictionary.Add(typeof(Question), new RepositoryBase<Question>(_ctx));
            if (!_repoDictionary.ContainsKey(typeof(QuestionType))) _repoDictionary.Add(typeof(QuestionType), new RepositoryBase<QuestionType>(_ctx));
        }

        #endregion

    }

}
