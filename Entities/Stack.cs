namespace Db1HealthPanelBack.Entities
{
    public class Stack
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<StackProject>? StackProjects { get; set; }
    }
}
