namespace SQLHydra.Models
{
    public class Column
    {
        public string Name { get; private set; }
        public string Alias { get; private set; }

        public Column(string name, string alias = null)
        {
            Name = name;
            Alias = alias;
        }
    }
}
