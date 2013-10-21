namespace SQLHydra.Models
{
    public class ComputedColumn : Column
    {
        public Constants.ComputationTypes ComputationType { get; private set; }

        public ComputedColumn(Constants.ComputationTypes computationType, string columnName, string alias) : base(columnName, alias)
        {
            ComputationType = computationType;
        }
    }
}
