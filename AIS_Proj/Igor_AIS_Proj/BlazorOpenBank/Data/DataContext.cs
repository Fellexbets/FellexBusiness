using BlazorOpenBank.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorOpenBank.Data
{
    

    namespace BlazorFileUpload.Server.Data
    {
        public class DataContext : DbContext
        {
            public DataContext(DbContextOptions<DataContext> options) : base(options)
            {

            }

            public DbSet<UploadResult> Uploads => Set<UploadResult>();
        }
    }
}
