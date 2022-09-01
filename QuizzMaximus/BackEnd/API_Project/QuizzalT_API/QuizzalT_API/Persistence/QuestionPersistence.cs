using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class QuestionPersistence : BasePersistence<Question>
    {
        public QuestionPersistence() => _contextEntity = _context.Questions;

        public override async Task<Question> Create(Question entity)
        {
            if (entity.QuestionImageString != null)
            {
                entity.QuestionImage = Convert.FromBase64String(entity.QuestionImageString);
            }

            try
            {
                await _context.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                return entity;
            }
        }
        public async Task<Question> UpdateQuestion(Question entity)
        {
            if (entity.QuestionImageString != null)
            {
                entity.QuestionImage = Convert.FromBase64String(entity.QuestionImageString);
            }
            else
            {
                entity.QuestionImage = null;
            }

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch
            {
                return null;
            }
        }
        public async Task<Question> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Exists(int id) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id));
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
        public async Task<List<Question>> GetAllQuestionsUser(int id) => await _contextEntity.AsNoTracking().Where(e => e.UserId == id).ToListAsync();
        public async Task<List<Question>> GetAllQuestionsAdmin() => await _contextEntity.AsNoTracking().Where(e => e.UserId == 1).ToListAsync();
    }
}
