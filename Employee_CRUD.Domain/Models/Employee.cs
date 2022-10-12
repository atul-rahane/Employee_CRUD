using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CRUD.Domain.Models
{
    public class Employee: IKeyedObject
    {
        public int? Id { get; set; } 
        public string Name { get; set; }

        public string Email { get; set; }   

        public string Gender { get; set; }
        public string Status { get; set; }

        public Employee(int? id, string name, string email, string gender, string status)
        {
            Id = id;
            Name = name;
            Email = email;
            Gender = gender;
            Status = status;
        }
    }
}


