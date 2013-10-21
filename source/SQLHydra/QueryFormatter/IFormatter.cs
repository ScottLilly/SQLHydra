namespace SQLHydra.QueryFormatter
{
    public interface IFormatter
    {
        string TableName { get; }
        string ColumnName { get; }

        string AllColumns { get; }

        string EqualTo { get; }
        string NotEqualTo { get; }
        string GreaterThan { get; }
        string GreaterThanOrEqualTo { get; }
        string LessThan { get; }
        string LessThanOrEqualTo { get; }
        string StartWith { get; }
        string EndsWith { get; }
        string Contains { get; }
        string WildCardCharacter { get; }
    }
}
