using Db1HealthPanelBack.Entities;
using Db1HealthPanelBack.Infra.EntityMaps;
using Microsoft.EntityFrameworkCore;

namespace Db1HealthPanelBack.Configs
{
    public class ContextConfig : DbContext
    {
        public required DbSet<Answer> Answers { get; set; }
        public required DbSet<AnswerQuestion> AnswersQuestions { get; set; }
        public required DbSet<AnswerPillar> AnswerPillars { get; set; }
        public required DbSet<Column> Column { get; set; }
        public required DbSet<Pillar> Pillars { get; set; }
        public required DbSet<Project> Projects { get; set; }
        public required DbSet<Question> Questions { get; set; }
        public required DbSet<Lead> Leads { get; set; }
        public required DbSet<LeadProject> LeadProject { get; set; }
        public required DbSet<Evaluation> Evaluations { get; set; }

        public ContextConfig(DbContextOptions options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new QuestionMap());
            modelBuilder.ApplyConfiguration(new ColumnMap());
            modelBuilder.ApplyConfiguration(new PillarMap());
            modelBuilder.ApplyConfiguration(new ProjectMap());
            modelBuilder.ApplyConfiguration(new AnswerMap());
            modelBuilder.ApplyConfiguration(new LeadMap());
            modelBuilder.ApplyConfiguration(new LeadProjectMap());
            modelBuilder.ApplyConfiguration(new EvaluationMap());
            modelBuilder.ApplyConfiguration(new AnswerPillarMap());
            modelBuilder.ApplyConfiguration(new AnswerQuestionMap());
        }
    }
}