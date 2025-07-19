using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Add properties for your repositories if you want to access them directly

        Task<int> CompleteAsync(); // Use async for database operations
        int Complete(); // Synchronous option
    }
}
