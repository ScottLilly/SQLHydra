namespace SQLHydra
{
    public static class QueryEngine
    {
        public static QueryBuilder ForDatabaseType(Constants.DBTypes databaseType)
        {

            return new QueryBuilder(databaseType);
        }
    }
}
