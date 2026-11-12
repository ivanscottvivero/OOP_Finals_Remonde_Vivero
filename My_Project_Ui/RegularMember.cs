namespace TechGuildManager
{
    public class RegularMember : GuildMember, ICompetitor
    {
        public RegularMember(int id, string name)
            : base(id, name)
        {
        }

        public override int CalculateEventPoints(int basePoints)
        {
            return basePoints;
        }

        public void JoinCompetition(string competitionName)
        {
            Console.WriteLine($"{Name} joined {competitionName}.");
        }
    }
}