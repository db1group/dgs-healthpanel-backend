namespace Db1HealthPanelBack.Entities
{
    public class StackProject
    {
        public Guid ProjectId { get; set; }
        public string StackId { get; set; }
        public bool Confirmed { get; set; }

        public Project? Project { get; set; }
        public Stack? Stack { get; set; }
    }
}
