namespace SQLHydra.Interfaces
{
    public interface ICanSetColumnValueToUpdate
    {
        ICanSetColumnValueToUpdateOrAddWhereCondition SetColumnToValue(string columnName, object value);
    }
}
