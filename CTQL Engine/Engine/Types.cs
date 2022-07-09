namespace CTQL_Engine
{
    internal static class Tokens
    {
        public static string TableName = "table_name";
        public static string Column = "column";
        public static string Condition = "condition";
        public static string Linker = "linker";
        public static string DefaultLinker = "default_linker";
        public static string CapsuleStart = "capsule_start";
        public static string CapsuleInner = "capsule_inner";
        public static string CapsuleEnd = "capsule_end";
        public static string Capsule = "capsule";
    }

    internal static class ConvertMap
    {
        public static Dictionary<string, string> Map = new Dictionary<string, string> {  
            { "!=", "<>" },
            { "~ (", "IN (" },
            { "~(", "IN (" },
            { "~", "LIKE" },
        };
    }

    internal static class QuerySyntax
    {
        public static string DefaultLinker = "and";
    }

    internal static class TranslatedLevl
    {
        public static int Tokenized = 1;
        public static int Condition = 2;
        public static int Columns = 2;
    }
}
