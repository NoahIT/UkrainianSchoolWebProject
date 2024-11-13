using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Subjects
{
    public class SubjectDAL : ISubjectDAL
    {
        private readonly PlatformDbContext context;

        public SubjectDAL(PlatformDbContext context)
        {
            this.context = context;
        }

        public async Task<List<OrderSubject>> GetByIds(List<int> ids)
        {
           List<OrderSubject> subjects = new List<OrderSubject>();

           foreach (int id in ids)
           {
               var subject = await context.OrderSubjects.FirstOrDefaultAsync(x => x.Id == id);

               if (subject != null)
               {
                   subjects.Add(subject);
               }
           }

           return subjects;
        }

        public async Task CreateOrderSubjects(int orderId, List<int> subjectsId)
        {
            foreach (var s in subjectsId)
            {
                await context.OrderSubjects.AddAsync(new OrderSubject()
                {
                    OrderId = orderId,
                    SubjectId = s
                });
            }

            await context.SaveChangesAsync();
        }
    }
}
