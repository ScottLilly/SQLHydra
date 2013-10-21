namespace SQLHydra.Interfaces
{
    public interface ICanSetColumnValueToUpdateOrAddWhereCondition : ICanSetColumnValueToUpdate
    {
        ICanGetSQLCommandObject ApplyToAllRows();
        ICanAddWhereConditionOrGetSQLCommandObject Where(string columnName, Constants.Comparators comparator, object value);
    }
}
