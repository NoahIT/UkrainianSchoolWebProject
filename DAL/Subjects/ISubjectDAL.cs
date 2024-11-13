using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Subjects
{
    public interface ISubjectDAL
    {
        Task<List<OrderSubject>> GetByIds(List<int> ids);

        Task CreateOrderSubjects(int orderId, List<int> subjectsId);
    }
}
