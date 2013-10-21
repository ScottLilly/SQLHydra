namespace SQLHydra
{
    public static class Constants
    {
        public enum DBTypes
        {
            StandardSQL,
            MSSQL2008
        }

        public enum Comparators
        {
            EqualTo,
            NotEqualTo,
            GreaterThan,
            GreaterThanOrEqualTo,
            LessThan,
            LessThanOrEqualTo,
            StartsWith,
            EndsWith,
            Contains
        }

        public enum ComputationTypes
        {
            Min,
            Max,
            CountOfAllRows,
            CountOfRowsWithNonNullColumnValues,
            CountOfRowsWithDistinctColumnValues,
            SumOfAllRows,
            SumOfRowsWithDistinctColumnValues,
            AverageOfAllRows,
            AverageOfRowsWithDistinctColumnValues
        }

        public enum OrderByDirections
        {
            Ascending,
            Descending
        }
    }
}
