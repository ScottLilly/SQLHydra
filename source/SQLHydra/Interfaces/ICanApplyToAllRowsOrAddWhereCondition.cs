namespace SQLHydra.Interfaces
{
    public interface ICanApplyToAllRowsOrAddWhereCondition
    {
        ICanGetSQLCommandObject ApplyToAllRows();
        ICanAddWhereConditionOrGetSQLCommandObject Where(string columnName, Constants.Comparators comparator, object value);
    }
}
