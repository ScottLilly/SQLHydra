namespace SQLHydra.Interfaces
{
    public interface ICanSetColumnValueToInsert
    {
        ICanSetColumnValueToInsertOrGetSQLCommandObject SetColumnToValue(string columnName, object value);
    }
}
