using EmployeeManagement.Application.Interfaces;

namespace EmployeeManagement.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Optionally, you can expose repositories here if you prefer a "UnitOfWork" pattern
        // where the UoW is the single entry point for all repositories.
        // private IEmployeeRepository _employeeRepository;
        // public IEmployeeRepository Employees => _employeeRepository ??= new EmployeeRepository(_context);

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
