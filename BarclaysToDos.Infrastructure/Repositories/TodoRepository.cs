using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarclaysToDos.Infrastructure.Repositories
{
    public class TodoRepository
    {
        public ToDoContext _context;

        public TodoRepository(ToDoContext toDoContext)
        {
            _context = toDoContext;
        }

        public IEnumerable<Vehicle> GetToDoList()
        {
            return _context.Vehicles;
        }

        public async Task<Vehicle> GetToDoItemAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> GetToDoItemAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }
        
        public async Task<Vehicle> AddToDoItemAsync(Vehicle vehicleData)
        {
            var response = await _context.Vehicles.AddAsync(vehicleData);
            _context.SaveChanges();
            return response.Entity;
        }
    }
}
