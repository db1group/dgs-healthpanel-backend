using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.EntityMaps;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Configs
{
    public class ContextConfig : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<AnswerQuestion> AnswersQuestions { get; set; }
        public DbSet<AnswerPillar> AnswerPillars { get; set; }
        public DbSet<Column> Column { get; set; }
        public DbSet<Pillar> Pillars { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadProject> LeadProject { get; set; }

        public ContextConfig(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionMap());
            modelBuilder.ApplyConfiguration(new ColumnMap());
            modelBuilder.ApplyConfiguration(new PillarMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new AnswerMap());
            modelBuilder.ApplyConfiguration(new LeadMap());
            modelBuilder.ApplyConfiguration(new LeadProjectMap());
            modelBuilder.ApplyConfiguration(new AnswerPillarMap());
            modelBuilder.ApplyConfiguration(new AnswerQuestionMap());
        }
    }
}