
namespace Igor_AIS_Proj.Persistence
{
    public class SessionPersistence : BasePersistence<Session>, ISessionPersistence
    {
        public SessionPersistence()
        {
            _contextEntity = _context.Sessions;
        }
        
        public async Task<Session> GetById(Guid id)
        {
            return _contextEntity.FirstOrDefault(x => x.SessionId == id);
        }
    }
}
