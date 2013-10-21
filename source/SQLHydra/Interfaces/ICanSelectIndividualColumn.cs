namespace SQLHydra.Interfaces
{
    public interface ICanSelectIndividualColumn : ICanAddJoin
    {
        ICanSelectIndividualColumn SelectColumn(string columnName);
        ICanSelectIndividualColumn SelectColumn(string columnName, string alias);

        ICanSelectIndividualColumnOrHavingClause SelectGroupByColumn(string columnName);
        ICanSelectIndividualColumnOrHavingClause SelectGroupByColumn(string columnName, string alias);

        ICanSelectIndividualColumn SelectMinValueForColumn(string columnName, string alias);
        ICanSelectIndividualColumn SelectMaxValueForColumn(string columnName, string alias);
        ICanSelectIndividualColumn SelectCountOfAllRows(string alias);
        ICanSelectIndividualColumn SelectCountOfRowsWithNonNullColumnValues(string columnName, string alias);
        ICanSelectIndividualColumn SelectCountOfRowsWithDistinctColumnValues(string columnName, string alias);
        ICanSelectIndividualColumn SelectSumOfAllRows(string columnName, string alias);
        ICanSelectIndividualColumn SelectSumOfRowsWithDistinctColumnValues(string columnName, string alias);
        ICanSelectIndividualColumn SelectAverageOfAllRows(string columnName, string alias);
        ICanSelectIndividualColumn SelectAverageOfRowsWithDistinctColumnValues(string columnName, string alias);
    }
}
