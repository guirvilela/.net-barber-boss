using BarberBoss.Domain.Repositories;

namespace BarberBoss.Infrastructure.DataAcess;

// Context para salvar os dados no banco definitivamente
public class UnitOfWork : IUnitOfWork
{
    private readonly BarberBossDbContext _dbContext;


    public UnitOfWork(BarberBossDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit()
    {
        await _dbContext.SaveChangesAsync();
    }
}
