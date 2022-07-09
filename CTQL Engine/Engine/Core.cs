namespace CTQL_Engine
{
    public class Query
    {
        private string ctql_query;
        private string sql_query;
        private string table = "table";
        private string[] query_parts;
        private string[] tokens;
        private string[] conditions;
        private string[] cols = new string[] { "*" };
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

        #region SQL
        public void setSQL(string sql)
        {
            this.sql_query = sql;
        }

        public string getSQL()
        {
            return this.sql_query;
        }
        #endregion

        #region Cols
        public void setCols(string[] cols)
        {
            this.cols = cols;
        }

        public string[] getCols()
        {
            return this.cols;
        }
        #endregion

        #region Table
        public void setTable(string tableName)
        {
            this.table = tableName;
        }

        public string getTable()
        {
            return this.table;
        }
        #endregion

        #region Condition
        public void setConditions(string[] conditions)
        {
            this.conditions = conditions;
        }

        public string[] getConditions()
        {
            return this.conditions;
        }
        #endregion

        #region Tokens
        public void setTokens(string[] tokens)
        {
            this.tokens = tokens;
        }

        public string[] getTokens()
        {
            return this.tokens;
        }
        #endregion

        #region Query Parts
        public void setQueryParts(string[] query_parts)
        {
            this.query_parts = query_parts;
        }

        public string[] getQueryParts()
        {
            return this.query_parts;
        }
        #endregion

        #region Level
        public void setLevel(int level)
        {
            this.level = level;
        }

        public int getLevel()
        {
            return this.level;
        }
        #endregion

    }
}
