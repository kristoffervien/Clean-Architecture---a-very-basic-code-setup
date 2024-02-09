using CleanArch.Domain.Common;
using CleanArch.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Threading.Tasks;
using System;

namespace CleanArch.Infra.Data.Common
{
    public abstract class BaseRepository<TEntity> : IPatternsFramework<TEntity>
        where TEntity : BaseAuditableEntity, new()

    {
        #region Fields

        protected readonly DbContext _dbContext;

        #endregion Fields

        #region Constructors

        public BaseRepository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this.Entity = new TEntity();
            this.Entities = new List<TEntity>();
        }

        #endregion Constructors

        #region Properties

        public List<TEntity> Entities { get; set; }
        public TEntity Entity { get; set; }

        #endregion Properties

        #region CRUD Methods

        public async Task AddAsync(TEntity entity)
        {
            this.Entity = entity;

            await this._dbContext.AddAsync<TEntity>(this.Entity);
        }

        public async Task AddRangeAsync(List<TEntity> entities)
        {
            await this._dbContext.AddRangeAsync(entities);
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await this._dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            this.Entity = await this._dbContext.FindAsync<TEntity>(id);

            return this.Entity;
        }

        public async Task RemoveAsync(int id)
        {
            await this.GetAsync(Convert.ToInt32(id));

            this._dbContext.Remove<TEntity>(this.Entity);
        }

        public async Task RemoveRangeAsync(string ids)
        {
            // Clear Entities
            this.Entities.Clear();

            // Loop for each ids..
            foreach (var id in ids.Split(","))
            {
                // Set Entity
                await this.GetAsync(Convert.ToInt32(id));

                // Add Entity to Entities
                this.Entities.Add(this.Entity);
            }

            this._dbContext.RemoveRange(this.Entities);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() =>
            {
                this.Entity = entity;

                this._dbContext.Update<TEntity>(this.Entity);
            });
        }

        #endregion CRUD Methods
    }
}