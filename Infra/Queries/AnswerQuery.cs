using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Infra.Queries
{
    public static class AnswerQuery
    {
        public static IQueryable<Answer> FetchWithQuestionAndPillars(this IQueryable<Answer> answer)
            => answer.Include(prop => prop.Pillars).Include(prop => prop.Questions);

        public static IQueryable<Answer> FetchWithMonthRange(this IQueryable<Answer> answer, bool? isRetroactive = false)
        {
            var actualMonth = DateTime.Now.Month;
            var lastMonth = DateTime.Now.AddMonths(-1).Month;
            
            if(isRetroactive == true) return answer.Where(p => lastMonth == p.CreatedAt.Month);

            return answer.Where(p => actualMonth == p.CreatedAt.Month || lastMonth == p.CreatedAt.Month);
        }

        public static IQueryable<Answer> WithProject(this IQueryable<Answer> answer, Guid id)
            => answer.Where(prop => prop.ProjectId == id);
    }
}