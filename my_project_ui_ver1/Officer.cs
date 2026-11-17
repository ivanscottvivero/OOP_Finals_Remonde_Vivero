namespace TechGuildManager
{
    public class Officer : GuildMember, IOrganizer
    {
        public Officer(int id, string name)
            : base(id, name) //s used to call the parent class's constructor or members, allowing child classes to reuse existing initialization and functionality instead of repeating code.
        {
        }

        public override int CalculateEventPoints(int basePoints)
        {
            return basePoints * 2;
        }

        public void OrganizeEvent(string eventName)
        {
            Console.WriteLine($"{Name} is organizing {eventName}.");
        }
    }
}