using System;
using System.Collections.Generic;
using System.Text;
using TP2.Models.Entities;

namespace TP2.Services.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);

        void Delete(T entity);
        void Add(T entity);
        void Update(T entity);
    }
}
