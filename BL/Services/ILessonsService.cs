using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace BL.Services
{
    public interface ILessonsService
    {
        Task<List<Lessons>> GetLessonsByAbbrAndValid(string abbr, int pageSize, int index);
        Task<List<Lessons>> GetLessonsByAbbrAndValid(int klass,string abbr, int pageSize, int index);
    }
}
