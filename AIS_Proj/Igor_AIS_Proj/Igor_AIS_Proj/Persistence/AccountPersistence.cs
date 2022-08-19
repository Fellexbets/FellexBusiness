using Igor_AIS_Proj.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using System.Linq;
using AutoMapper.EF6;

namespace Igor_AIS_Proj.Persistence
{
    public class AccountPersistence : BasePersistence<Account>
    {
        public AccountPersistence() => _contextEntity = _context.Accounts;


        public Account GetById(int id) =>  _contextEntity.Include(a => a.Movements).FirstOrDefault(a => a.AccountId == id);

        public async Task<bool> Delete(int id) => await Delete(_contextEntity.Find(id));

        public void Create(CreateAccountRequest request)
        {
            _contextEntity.Add(request);
        }
    }
}
