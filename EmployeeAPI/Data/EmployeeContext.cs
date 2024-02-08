using EmployeeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Data;

public class EmployeeContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EmployeesDatabase;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>()
             .HasData(GetInitialEmployees());
    }

    private List<Employee> GetInitialEmployees() 
    {
        var employees = new List<Employee>()
        {
            new Employee()
            {
                EmployeeId = 1,
                FirstName = "Иван",
                LastName = "Антюхов",
                Department = "Администраиця",
                Salary = 50000
            },
            new Employee()
            {
                EmployeeId = 2,
                FirstName = "Иван",
                LastName = "Чеботарев",
                Department = "Администраиця",
                Salary = 45000
            },
            new Employee()
            {
                EmployeeId = 3,
                FirstName = "Никита",
                LastName = "Ходяков",
                Department = "Гендиректор",
                Salary = 850000
            },
            new Employee()
            {
                EmployeeId = 4,
                FirstName = "Екатерина",
                LastName = "Разимович",
                Department = "It",
                Salary = 50000
            },

        };
        return employees;
    }
}
