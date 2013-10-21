namespace SQLHydra.Models
{
    public class Join
    {
        public string TableName { get; private set; }
        public string BaseTableColumnName { get; private set; }
        public string JoinedTableColumnName { get; private set; }

        public Join(string tableName, string baseTableColumnName, string joinedTableColumnName)
        {
            TableName = tableName;
            BaseTableColumnName = baseTableColumnName;
            JoinedTableColumnName = joinedTableColumnName;
        }
    }
}
