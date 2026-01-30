namespace WebApp
{
    public class Employee
    {
        public int id { get; set; }
        public string name { get; set; }
        public int salary { get; set; }
        public string role { get; set; }

        public Employee(int id,string name,int salary, string role)
        {
            this.id = id; 
            this.name = name;
            this.salary = salary;
            this.role = role;
        }
    }
}
