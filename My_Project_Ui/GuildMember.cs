namespace TechGuildManager
{
    public abstract class GuildMember
    {
        // Encapsulation: fields are private
        private int _id;
        private string _name;
        private string _status;
        private int _totalPoints;

        // Properties control access to private fields
        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
            }
        }

        public string Status
        {
            get { return _status; }
            protected set { _status = value; }
        }

        public int TotalPoints
        {
            get { return _totalPoints; }
            protected set
            {
                if (value >= 0)
                {
                    _totalPoints = value;
                }
            }
        }

        // Constructor
        protected GuildMember(int id, string name)
        {
            _id = id;
            _name = name;
            _status = "Active";
            _totalPoints = 0;
        }

        // Polymorphism: derived classes provide their own implementation
        public abstract int CalculateEventPoints(int basePoints);

        public virtual void DisplayInformation()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Total Points: {TotalPoints}");
        }
    }
}