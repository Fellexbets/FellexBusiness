using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class QuizzQuestionPersistence : BasePersistence<QuizzQuestion>
    {
        public QuizzQuestionPersistence() => _contextEntity = _context.QuizzQuestions;

        public async Task<QuizzQuestion> GetById(int id1, int id2) => await _contextEntity.FindAsync(id1, id2);
        public async Task<bool> Exists(int id1, int id2) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id1) && e.ReturnId().Contains(id2));
        public async Task<bool> Delete(int id1, int id2) => await Delete(_contextEntity.Find(id1, id2));
        public async Task<List<QuizzQuestion>> CreateQuizzQuestions(List<QuizzQuestion> quizzQuestions)
        {
            try
            {
                foreach (QuizzQuestion quizzQuestion in quizzQuestions)
                {
                    await _contextEntity.AddAsync(quizzQuestion);
                    await _context.SaveChangesAsync();
                }
                return quizzQuestions;
            }
            catch
            {
                return quizzQuestions;
            }
        }
        public async Task<List<QuizzQuestion>> GetAllQuizzQuestionsUser(int id)
        {
            try
            {
                List<Quizz> quizzes = await _context.Quizzes.Where(q => q.UserId == id).ToListAsync();

                List<QuizzQuestion> quizzQuestions = new List<QuizzQuestion>();

                foreach (Quizz quizz in quizzes)
                {
                    List<QuizzQuestion> quizzQuestionsToAdd = await _contextEntity.Where(q => q.QuizzId == quizz.QuizzId).ToListAsync();
                    foreach (QuizzQuestion quizzQuestion in quizzQuestionsToAdd)
                    {
                        quizzQuestions.Add(quizzQuestion);
                    }
                }
                return quizzQuestions;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<QuizzQuestion>> GetAllQuizzQuestionsAdmin()
        {
            try
            {
                List<Quizz> quizzes = await _context.Quizzes.Where(q => q.UserId == 1).ToListAsync();

                List<QuizzQuestion> quizzQuestionsAdmin = new List<QuizzQuestion>();
                foreach (Quizz quizz in quizzes)
                {
                    List<QuizzQuestion> quizzQuestions = _contextEntity.Where(x => x.QuizzId == quizz.QuizzId).ToList();

                    foreach(QuizzQuestion quizzQuestion in quizzQuestions)
                    {
                        quizzQuestionsAdmin.Add(quizzQuestion);
                    }
                }
                return quizzQuestionsAdmin;
            }
            catch
            {
                return null;
            }
        }
    }
}