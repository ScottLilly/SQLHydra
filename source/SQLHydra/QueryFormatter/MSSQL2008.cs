namespace SQLHydra.QueryFormatter
{
    public class MSSQL2008 : StandardSQL, IFormatter
    {
        public new string TableName
        {
            get { return "[{0}]"; }
        }

        public new string ColumnName
        {
            get { return "[{0}]"; }
        }

        public new string NotEqualTo
        {
            get { return " != "; }
        }

    }
}
