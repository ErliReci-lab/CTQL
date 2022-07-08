namespace CTQL_Engine
{
    internal static class Tokens
    {
        public static string TableName = "table_name";
        public static string Column = "column";
        public static string Condition = "condition";
        public static string Linker = "linker";
        public static string CapsuleStart = "capsule_start";
        public static string CapsuleInner = "capsule_inner";
        public static string CapsuleEnd = "capsule_end";
    }

    internal static class QuerySyntax
    {
        public static string DefaultLinker = "<default_linker>";
    }

    internal static class TranslatedLevl
    {
        public static int Tokenized = 1;
    }
}
