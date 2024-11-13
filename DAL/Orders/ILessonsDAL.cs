using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Orders
{
    public interface ILessonsDAL
    {
        Task<List<Lessons>> GetAllLessonsByAbbr(int klass, string abbr, int PageSize, int pageIndex);
        Task<List<Lessons>> GetAllLessonsByAbbr(string abbr, int PageSize, int pageIndex);
        Task<List<Lessons>> GetAllLessonsByAbbrAndValid(int userId, string abbr, int pageSize, int currIndex);
        Task<List<Lessons>> GetAllLessonsByAbbrAndValid(int klass,int userId, string abbr, int pageSize, int currIndex);
    }
}
