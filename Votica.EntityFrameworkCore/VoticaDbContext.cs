using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Votica.EntityFrameworkCore.Configurations;
using Votica.Database.Generics;
using Votica.Domain;

namespace Votica.EntityFrameworkCore
{
    /// <summary>
    /// Votica Entity EFCore Database Context
    /// </summary>
    public class VoticaDbContext : DbContext, IDatabaseContext
    {
        public VoticaDbContext(DbContextOptions<VoticaDbContext> options)
            : base(options)
        {
            
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await base.SaveChangesAsync();
        }

        public async Task<T> FindByAsync<T>(Expression<Func<T, bool>> match) where T : class
        {
            return await base.Set<T>().SingleOrDefaultAsync(match);
        }

        public IQueryable<T> FindWhere<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            return base.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll<T>() where T : class
        {
            return base.Set<T>();
        }

        public async Task<T> GetByIdAsync<T>(object key) where T : class
        {
            return await base.Set<T>().FindAsync(key);
        }

        public async Task<T> InsertAsync<T>(T entity) where T : class
        {
            await base.Set<T>().AddAsync(entity);
            await base.SaveChangesAsync();
            return entity;
        }

        public async Task<int> RemoveAsync<T>(T entity, object key) where T : class
        {
            base.Set<T>().Remove(entity);
            return await base.SaveChangesAsync();
        }

        public async Task<T> UpdateAsync<T>(T entity, object key) where T : class
        {
            if (entity == null)
                return null;

            T entityExist = await base.Set<T>().FindAsync(key);
            if (entityExist != null)
            {
                base.Entry(entityExist).CurrentValues.SetValues(entity);
                await base.SaveChangesAsync();
            }
            return entityExist;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .ApplyConfiguration<Poll>(new PollConfiguration())
                .ApplyConfiguration<QuestionType>(new QuestionTypeConfiguration())
                .ApplyConfiguration<Question>(new QuestionConfiguration())
                .ApplyConfiguration<Option>(new OptionConfiguration())
                .ApplyConfiguration<ParticipantOption>(new ParticipantOptionConfiguration())
                .ApplyConfiguration<Participant>(new ParticipantConfiguration());
                
        }

    }
}