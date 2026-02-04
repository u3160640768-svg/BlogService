namespace Domain
{
    
    public abstract class Command
    {
        // Proprietà comune a tutti i comandi/log
        public DateTime ExecutionTime { get; }
        public string Description { get; }

        protected Command(string description, DateTime exTime)
        {
            Description = description;
            ExecutionTime = exTime;
        }

        // Metodo astratto per definire come ogni comando deve presentarsi
        public abstract string GetDetails();
    }
}
