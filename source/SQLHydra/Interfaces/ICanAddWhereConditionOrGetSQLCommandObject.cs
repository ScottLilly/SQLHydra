namespace SQLHydra.Interfaces
{
    public interface ICanAddWhereConditionOrGetSQLCommandObject : ICanGetSQLCommandObject
    {
        ICanAddWhereConditionOrGetSQLCommandObject Where(string columnName, Constants.Comparators comparator, object value);
    }
}
