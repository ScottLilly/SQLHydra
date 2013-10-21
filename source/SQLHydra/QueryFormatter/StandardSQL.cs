namespace SQLHydra.QueryFormatter
{
    public class StandardSQL : IFormatter
    {
        public string TableName
        {
            get { return "{0}"; }
        }

        public string ColumnName
        {
            get { return "{0}"; }
        }

        public string AllColumns
        {
            get { return "*"; }
        }

        public string EqualTo
        {
            get { return " = "; }
        }

        public string NotEqualTo
        {
            get { return " <> "; }
        }

        public string GreaterThan
        {
            get { return " > "; }
        }

        public string GreaterThanOrEqualTo
        {
            get { return " >= "; }
        }

        public string LessThan
        {
            get { return " < "; }
        }

        public string LessThanOrEqualTo
        {
            get { return " <= "; }
        }

        public string StartWith
        {
            get { return " LIKE "; }
        }

        public string EndsWith
        {
            get { return " LIKE "; }
        }

        public string Contains
        {
            get { return " LIKE "; }
        }

        public string WildCardCharacter
        {
            get { return "%"; }
        }
    }
}
