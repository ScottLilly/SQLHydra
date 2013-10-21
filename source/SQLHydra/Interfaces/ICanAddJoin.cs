namespace SQLHydra.Interfaces
{
    public interface ICanAddJoin : ICanAddWhereCondition
    {
        ICanAddJoin InnerJoin(string tableName, string baseTableColumnName, string joinedTableColumnName);
        ICanAddJoin LeftJoin(string tableName, string baseTableColumnName, string joinedTableColumnName);
    }
}
