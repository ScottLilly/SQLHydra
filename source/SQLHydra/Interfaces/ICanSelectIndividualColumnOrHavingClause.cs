namespace SQLHydra.Interfaces
{
    public interface ICanSelectIndividualColumnOrHavingClause : ICanSelectIndividualColumn
    {
        ICanSelectIndividualColumnOrHavingClause HavingMinValueForColumn(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingMaxValueForColumn(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingCountOfAllRows(Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingCountOfRowsWithNonNullColumnValues(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingCountOfRowsWithDistinctColumnValues(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingSumOfColumnForAllRows(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingSumOfColumnsWithDistinctValues(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingAverageOfColumnForAllRows(string columnName, Constants.Comparators comparator, double value, string alias);
        ICanSelectIndividualColumnOrHavingClause HavingAverageOfColumnsWithDistinctValues(string columnName, Constants.Comparators comparator, double value, string alias);
    }
}
