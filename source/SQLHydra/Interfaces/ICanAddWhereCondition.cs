namespace SQLHydra.Interfaces
{
    public interface ICanAddWhereCondition : ICanAddOrderByClause
    {
        ICanAddWhereCondition Where(string columnName, Constants.Comparators comparator, object value);
    }
}
