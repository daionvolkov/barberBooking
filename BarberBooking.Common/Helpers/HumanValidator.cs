using Microsoft.EntityFrameworkCore;

namespace BarberBooking.Common.Helpers
{
    public static class HumanValidator
    {
        public static async Task<bool> ExistsAsync<TEntity>(this DbContext dbContext, object id) where TEntity : class
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            var dbSet = dbContext.Set<TEntity>();

            var entity = await dbSet.FindAsync(id);
            return entity != null;
        }
    }
}
