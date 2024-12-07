﻿using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.CategoryRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAll();
        Task<Category> GetById(Guid id);
    }
}
