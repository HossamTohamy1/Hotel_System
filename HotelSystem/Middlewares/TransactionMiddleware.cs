using Hotel.Core.Entities;
using Hotel.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Middlewares
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
       

        public async Task Invoke(HttpContext context, HotelDbContext dbContext)
        {
            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbContext.Database.BeginTransactionAsync();

                try
                {
                    await _next(context);
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }
    }
}
