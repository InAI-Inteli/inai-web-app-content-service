using Microsoft.EntityFrameworkCore;
using WebAPIContentService.Infra.Data.Context;

namespace WebAPIContentService.Infra.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContentContext _context;

        public UnitOfWork(ContentContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Rollback()
        {

        }
    }
}
