using EmployeeAPI.Controllers.Models;
using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeContext context;

    public EmployeeController(EmployeeContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Post(EmployeeCreateDTO employeedto)
    {
        var employee = new Employee()
        {
            FirstName = employeedto.FirstName,
            LastName = employeedto.LastName,
            Department = employeedto.Department,
            Salary = employeedto.Salary
        };

        context.Employees.Add(employee);
        await context.SaveChangesAsync();

        return Ok(employee);
    }

    [HttpGet("/{id}")]
    public ActionResult<Employee> GetById(int id)
    {
        var res = context.Employees.Find(id);
        if (res != null)
        {
            return Ok(res);
        }
        return NotFound();
    } 

    [HttpGet]
    public async Task<IEnumerable<Employee>> GetAll(string? name = null, string? lastName = null, int? salary = null)
    {
        return await context.Employees.Where(
            x => x.FirstName == name 
            || x.LastName == lastName
            || x.Salary == salary).ToListAsync();
    }

    [HttpGet]
    [Route("/page")]
    public async Task<IEnumerable<Employee>> GetAll([FromQuery] int pageSize, [FromQuery] int? page)
    {
        return await context.Employees.Skip((page ?? 0) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    [HttpDelete]
    public async Task<ActionResult> Delete(int id)
    {
        var findEmp = context.Employees.Find(id);
        if (findEmp != null)
        {
            context.Employees.Remove(findEmp);
            return Ok();
        }
        return BadRequest();
    }
}




