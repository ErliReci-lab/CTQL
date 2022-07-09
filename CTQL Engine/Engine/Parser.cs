using System.Text.RegularExpressions;

namespace CTQL_Engine
{

    internal class ConditionNode
    {
        private string? originalCondition;
        private string? SQLCondition;
        public string? linker;
        public ConditionNode? nextNode = null;

        public ConditionNode getLast()
        {
            ConditionNode temp = this;
            while (temp.nextNode != null)
            {
                temp = temp.nextNode;
            }
            return temp;
        }

        public void setCondition(string condition)
        {
            this.originalCondition = condition;
            condition = condition.Replace("\"", "'");
            foreach (KeyValuePair<string, string> entry in ConvertMap.Map)
            {
                condition = condition.Replace(entry.Key, entry.Value);
            }
            this.SQLCondition = condition;
        }

        public string? getCondition(bool original = false)
        {
            return original ? this.originalCondition : this.SQLCondition;
        }
    }

    public static class Parser
    {
        internal static ConditionNode buildTree(string query)
        {
            ConditionNode root = new ConditionNode();
            query = query.Trim();
            string[] tokens = Regex.Split(query, "(and|or)"); 
            for (int i = 0; i < tokens.Length; i++)
            {
                ConditionNode tempNode = new ConditionNode();
                tempNode.setCondition(tokens[i].Trim());

                if (i + 1 < tokens.Length)
                {
                    tempNode.linker = tokens[i + 1].ToUpper();
                    i += 1;
                }

                root.getLast().nextNode = tempNode;
            }
            return root;
        }

        public static string buildQuery(Query query)
        {
            ConditionNode root = buildTree(String.Join(" ", query.getConditions()));
            string sqlQuery = $"SELECT {String.Join(", ", query.getCols())} FROM {query.getTable()}";
            if (query.getConditions().Length > 0)
            {
                sqlQuery += " WHERE ";
                ConditionNode? temp = root.nextNode;
                while (temp != null)
                {
                    sqlQuery += temp.getCondition();
                    if (temp.nextNode != null)
                    {
                        sqlQuery += $" {temp.linker} ";
                    }
                    temp = temp.nextNode;
                }
            }
            query.setSQL(sqlQuery);
            return sqlQuery;
        }

    }
}
