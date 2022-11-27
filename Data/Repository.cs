using Core.Repositories;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;



namespace Core.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        //
        public async Task DeleteByIdAsync(object id)
        {
           
            var obj = await dbSet.FindAsync(id);
            await Task.Run(()=> dbSet.Remove(obj)); 
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params string[] includes)
        {
            IQueryable<TEntity> query = dbSet;

            foreach (var item in includes)
            {
                query=query.Include(item);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
           await Task.Run(() => dbSet.Update(entity));
        }

        public async Task InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }

        
    }
}
