namespace SQLHydra.Models
{
    public class AssignmentColumn
    {
        public string ColumnName { get; private set; }
        public object Value { get; private set; }

        public AssignmentColumn(string columnName, object value)
        {
            ColumnName = columnName;
            Value = value;
        }
    }
}
