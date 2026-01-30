using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
//HTTP GET Method
app.Run(async (HttpContext context) =>
{
    // To use html :
    //context.Response.Headers["Content-Type"] = "text/html";

    if (context.Request.Path.StartsWithSegments("/"))
    {
        await context.Response.WriteAsync($" The Method is: {context.Request.Method}\r\n");
        await context.Response.WriteAsync($"The url is: {context.Request.Path}\r\n");
        //await context.Response.WriteAsync($"Header: {context.Request.Headers.Keys}\r\n");

        await context.Response.WriteAsync($"\r\nHeaders:\r\n");
        foreach (var key in context.Request.Headers.Keys)
        {
            await context.Response.WriteAsync($"{key} : {context.Request.Headers[key]}\r\n");
        }

    }
    // Handling the Endpoints
    else if (context.Request.Path.StartsWithSegments("/employees"))
    {
        //GET
        if (context.Request.Method == "GET")
        {
            if(context.Request.Query.ContainsKey("id"))
            {
                var Id = context.Request.Query["id"];
                if (int.TryParse(Id, out int employeeId))
                {
                    var emp = EmployeeRepository.GetEmployee(employeeId);
                    if (emp is not null)
                    {
                        await context.Response.WriteAsync($"{emp.name} : {emp.position} : {emp.salary}");
                    }
                    else
                    {
                        context.Response.StatusCode = 404;
                        await context.Response.WriteAsync("Employee Not found!");
                    }
                }
            }
            else
            {
                var employees = EmployeeRepository.GetAllEmployees();
                foreach (var employee in employees)
                {
                    await context.Response.WriteAsync($"{employee.name} : {employee.position}\r\n");
                }
            }
            context.Response.StatusCode = 200;
        }
        //POST
        else if (context.Request.Method == "POST")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            try
            {
                var employee = JsonSerializer.Deserialize<Employee>(body);
                if (employee is null || employee.Id <= 0)
                {
                    context.Response.StatusCode = 400;
                    return;
                }
                EmployeeRepository.AddEmployee(employee);
                context.Response.StatusCode = 201; // 201 for POST
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 400;
                return;
            }            
        }
        //PUT
        else if (context.Request.Method == "PUT")
        {
            using var reader = new StreamReader(context.Request.Body);
            var body = await reader.ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(body);

            var result = EmployeeRepository.UpdateEmployee(employee);

            if (result)
            {
                context.Response.StatusCode = 204; // 204 for PUT
                await context.Response.WriteAsync("Employee updated successfully!");
                return;
            }
            else
            {
                await context.Response.WriteAsync("Employee not found!");
            }
        }
        //DELETE
        else if (context.Request.Method == "DELETE")
        {
            if (context.Request.Query.ContainsKey("id"))
            {
                var id = context.Request.Query["id"]; // Got 'id' as a string
                if (int.TryParse(id, out int employeeId))
                {
                    var result = EmployeeRepository.DeleteEmployee(employeeId);
                    if (result)
                    {
                        await context.Response.WriteAsync("Employee Deleted successfully!");
                    }
                    else
                    {
                        context.Response.StatusCode = 404; 
                        await context.Response.WriteAsync("Employee not found!");
                    }  
                }
            }
        }
    }
    else
    {
        context.Response.StatusCode = 404;
    }
        //QUERY STRING
        foreach (var key in context.Request.Query.Keys)
        {
            await context.Response.WriteAsync($"{key} : {context.Request.Query[key]}\r\n");
        } 
});

app.Run();


static class EmployeeRepository
{
    private static List<Employee> employees = new List<Employee>
    {
        new Employee(1,"John","Engineer",500000),
        new Employee(2,"Smith","Technician",300000),
        new Employee(3,"Sam","HR",600000),
    };

    public static List<Employee> GetAllEmployees()
    {
        return employees;
    }

    public static Employee GetEmployee(int id)
    {
        var employee = employees.FirstOrDefault(x => x.Id == id);
        if (employee is not null) return employee;
        else return null;
    }
    public static void AddEmployee(Employee? emp)
    {
        if (emp != null)
        {
            employees.Add(emp);
        }
    }
    public static bool UpdateEmployee(Employee? emp)
    {
        if(emp is not null)
        {
            var employee = employees.FirstOrDefault(x => x.Id == emp.Id);
            if(employee is not null)
            {
                employee.name = emp.name;
                employee.position = emp.position;
                employee.salary = emp.salary;
                return true;
            }
        }
        return false;
    }
    public static bool DeleteEmployee(int employeeId)
    {
        var employee = employees.FirstOrDefault(x => x.Id == employeeId);
        if(employee is not null)
        {
            employees.Remove(employee);
            return true;
        }
        return false;
    }
}

public class Employee
{
    public int Id { get; set; }
    public string name { get; set; }
    public string position { get; set; }
    public int salary { get; set; }

    public Employee(int id,string name, string position, int salary)
    {
        this.Id = id;
        this.name = name;
        this.position = position;
        this.salary = salary;
    }
}
