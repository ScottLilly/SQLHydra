namespace SQLHydra.Models
{
    public class WhereCondition
    {
        public string ColumnName { get; private set; }
        public Constants.Comparators Comparator { get; private set; }
        public object Value { get; private set; }

        public WhereCondition(string columnName, Constants.Comparators comparator, object value)
        {
            ColumnName = columnName;
            Comparator = comparator;
            Value = value;
        }
    }
}
