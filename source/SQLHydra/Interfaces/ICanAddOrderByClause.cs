namespace SQLHydra.Interfaces
{
    public interface ICanAddOrderByClause : ICanGetSQLCommandObject
    {
        ICanAddOrderByClause OrderBy(string columnName, Constants.OrderByDirections orderByDirection);
    }
}
