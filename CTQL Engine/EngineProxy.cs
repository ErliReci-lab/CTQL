namespace CTQL_Engine
{
    public class EngineProxy
    {
        public bool log = false;
        public bool forceTable = false;

        public string CTQL_SQL(string queryCTQL)
        {
            if (forceTable)
                queryCTQL = "table " + queryCTQL;

            Query query = new(queryCTQL);
            if (log)
            {
                Console.WriteLine("Query> " + query.getCTQL());
                Console.WriteLine("------");
            }

            string[] token = Lexer.tokenize(query, true);
            if (log)
            {
                Console.WriteLine("Tokens> " + String.Join(" ", token));
                Console.WriteLine("-------");
            }

            string[] conditions = Lexer.condtion(query);
            if (log)
            {
                Console.WriteLine("Condtions> " + String.Join(" ", conditions));
                Console.WriteLine("----------");
            }

            string[] columns = Lexer.columns(query);
            if (log)
            {
                Console.WriteLine("Columns> " + String.Join(" ", columns));
                Console.WriteLine("--------");
            }

            Parser.buildQuery(query);

            return query.getSQL();
        }
    }
}
