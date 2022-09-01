using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class AnswerPersistence : BasePersistence<Answer>
    {
        public AnswerPersistence() => _contextEntity = _context.Answers;

        public async Task<Answer> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Exists(int id) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id));
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
        public async Task<List<Answer>> CreateAnswers(List<Answer> answers)
        {
            try
            {
                foreach (Answer answer in answers)
                {
                    await _contextEntity.AddAsync(answer);
                    await _context.SaveChangesAsync();
                }
                return answers;
            }
            catch
            {
                return answers;
            }
        }
        public async Task<bool> UpdateAnswers(List<Answer> answers)
        {
            try
            {
                foreach (Answer answer in answers)
                {
                    _contextEntity.Update(answer);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Answer>> GetAnswersByUser(int userId)
        {
            try
            {
                List<Question> questions = await _context.Questions
                    .Where(q => q.UserId == userId).ToListAsync();

                List<Answer> answers = new List<Answer>();

                foreach (Question question in questions)
                {
                    List<Answer> answersToAdd = await _contextEntity.Where(q => q.QuestionId == question.QuestionId).ToListAsync();
                    foreach (Answer answer in answersToAdd)
                    {
                        answers.Add(answer);
                    }
                }
                return answers;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Answer>> GetAllAnswersAdmin()
        {
            try
            {
                List<Question> questions = await _context.Questions.AsNoTracking().Where(e => e.UserId == 1).ToListAsync();

                List<Answer> answers = new List<Answer>();

                foreach (Question question in questions)
                {
                    List<Answer> answersToAdd = await _contextEntity.Where(q => q.QuestionId == question.QuestionId).ToListAsync();
                    foreach (Answer answer in answersToAdd)
                    {
                        answers.Add(answer);
                    }
                }
                return answers;
            }
            catch
            {
                return null;
            }
        }
    }
}
