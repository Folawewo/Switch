namespace @switch.domain.Entities
{
    public class SwitchToggle : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool IsEnabled { get; set; }
        public string TargetingRules { get; set; }
    }
}