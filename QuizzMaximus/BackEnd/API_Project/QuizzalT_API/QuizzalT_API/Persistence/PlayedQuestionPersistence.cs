using Microsoft.EntityFrameworkCore;
using QuizzalT_API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzalT_API.Persistence
{
    public class PlayedQuestionPersistence : BasePersistence<PlayedQuestion>
    {
        public PlayedQuestionPersistence() => _contextEntity = _context.PlayedQuestions;

        public async Task<PlayedQuestion> GetById(int id) => await _contextEntity.FindAsync(id);
        public async Task<bool> Exists(int id) => await _contextEntity.AnyAsync(e => e.ReturnId().Contains(id));
        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));
        public async Task<List<PlayedQuestion>> CreatePlayedQuestions(List<PlayedQuestion> playedQuestions)
        {
            try
            {
                playedQuestions.ForEach(playedQuestion => _context.AddAsync(playedQuestion));
                await _context.SaveChangesAsync();
                return playedQuestions;
            }
            catch
            {
                return playedQuestions;
            }
        }
    }
}
