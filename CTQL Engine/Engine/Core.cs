namespace CTQL_Engine
{
    public class Query
    {
        private string ctql_query;
        private string sql_query;
        private string[] query_parts;
        private string[] tokens;
        private int level = 0;

        public Query(string ctql_query)
        {
            this.ctql_query = ctql_query;
            this.query_parts = ctql_query.Split();
        }

        public string getCTQL()
        {
            return this.ctql_query;
        }

        public string getSQL()
        {
            return this.sql_query;
        }

        public void setTokens(string[] tokens)
        {
            this.tokens = tokens;
        }

        public string[] getTokens()
        {
            return this.tokens;
        }

        public void setQueryParts(string[] query_parts)
        {
            this.query_parts = query_parts;
        }   
        
        public string[] getQueryParts()
        {
            return this.query_parts;
        }

        public int getLevel()
        {
            return this.level;
        }

        public void setLevel(int level)
        {
            this.level = level;
        }
    }
}
