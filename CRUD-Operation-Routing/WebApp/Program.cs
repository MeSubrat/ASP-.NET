using System.Text.Json;
using WebApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

//Get All employees;
app.MapGet("/employees", async (HttpContext context) =>
{
    var employees = EmployeeRepository.GetEmployees();
    context.Response.StatusCode = 200;
    foreach (var employee in employees)
    {
        await context.Response.WriteAsync($"{employee.id} : {employee.name} : {employee.role} : {employee.salary}\r\n");
    }
    
});

//Create an employee
app.MapPost("/employees", async (HttpContext context) =>
{
    using var reader = new StreamReader(context.Request.Body);
    var body = await reader.ReadToEndAsync();

    try
    {
        var employee = JsonSerializer.Deserialize<Employee>(body);
        if(employee == null)
        {
            context.Response.StatusCode = 400;
            return;
        }
        EmployeeRepository.CreateEmployee(employee);
        context.Response.StatusCode = 201;
        await context.Response.WriteAsync("Employee Creation Success");
    }
    catch(Exception ex)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Error while creating Employee");
    }
});

//Get Employee by id
app.MapGet("/employees/{id}", async (context) =>
{

});



app.Run();

public static class EmployeeRepository
{
    private static List<Employee> employees = new List<Employee>
    {
        new Employee ( 1, "Amit", 40000,"HR"),
        new Employee(  2,  "Riya", 60000, "IT" ),
        new Employee(  3, "Rahul", 50000 , "Finance")
    };
        
    public static bool CreateEmployee(Employee employee)
    {
        int id = employees.Count == 0 ? 1 : employees.Count+1;
        if(employees is not null)
        {
            employees.Add(new Employee(id, employee.name, employee.salary, employee.role));
            return true;
        }
        return false;
    }

    public static List<Employee> GetEmployees()
    {
        return employees;
    }
    public static Employee GetEmployeeById(int id)
    {
        return employees.FirstOrDefault(x => x.id == id);
    }
    public static bool DeleteEmployee(int id)
    {
        var employee = employees.FirstOrDefault(x => x.id == id);
        if(employee is not null)
        {
            employees.Remove(employee);
        }
        return false;
    }
    public static bool UpdateEmployee(int id,string name,int salary,string role)
    {
        var employee = employees.FirstOrDefault(x => x.id == id);
        if(employee is not null)
        {
            employee.name = name;
            employee.role = role;
            employee.salary = salary;
            return true;
        }
        return false;
    }
}