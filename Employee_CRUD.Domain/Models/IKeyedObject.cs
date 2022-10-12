using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_CRUD.Domain.Models
{
    public interface IKeyedObject
    {
        int? Id { get; set; }
    }
}
