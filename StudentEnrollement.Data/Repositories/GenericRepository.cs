﻿using Microsoft.EntityFrameworkCore;
using StudentEnrollement.Data.Contracts;
using StudentEnrollement.Data.DatabaseContext;

namespace StudentEnrollement.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly StudentEnrollementDbContext _db;

        public GenericRepository(StudentEnrollementDbContext db)
        {
            _db = db;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            //To Do is null or Not
            _db.Set<TEntity>().Remove(entity);
            //return boolean : is succuced or not
            return await _db.SaveChangesAsync() > 0;  
        }

        public async Task<bool> Exists(int id)
        {
            //var entity = await GetAsync(id);
            //return entity != null;
            return await _db.Set<TEntity>().AnyAsync(p => p.Id ==id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _db.Set<TEntity>().ToListAsync(); 
        }

        public async Task<TEntity?> GetAsync(int id)
        {
            //Fix if result is null
            var result = await _db.Set<TEntity>().FindAsync(id);
            return result;
        }

        public async Task UpdateAsync(TEntity entity)
        {          
            //_db.Set<TEntity>().Update(entity);
            _db.Update(entity);
            await _db.SaveChangesAsync();           
        }
    }

}
