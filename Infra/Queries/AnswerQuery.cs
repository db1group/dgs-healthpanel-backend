using Db1HealthPanelBack.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Infra.Queries
{
    public static class AnswerQuery
    {
        public static IQueryable<Answer> FetchWithQuestionAndPillars(this IQueryable<Answer> answer)
            => answer.Include(prop => prop.Pillars).Include(prop => prop.Questions);

        public static IQueryable<Answer> FetchWithMonthRange(this IQueryable<Answer> answer)
        {
            var actualMonth = DateTime.Now.Month;
            var lastMonth = DateTime.Now.AddMonths(-1).Month;

            return answer.Where(p => actualMonth == p.CreatedAt.Month || lastMonth == p.CreatedAt.Month);
        }
    }
}