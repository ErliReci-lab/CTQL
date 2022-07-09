namespace CTQL_Engine
{
    public static class Lexer
    {
        private static string[] syntax = { "=", "~", ">", "<", "<=", ">=", "!=", "!~" };
        private static string[] linker = { "and", "or" };
        private static string[] capsule = { "(", ")" };

        public static string[] condtion(Query query)
        {
            if (query.getLevel() < TranslatedLevl.Tokenized) throw new Exception("Run tokenize before condtion.");

            List<string> condtions = new List<string>();
            List<string> conditionsTokens = new List<string>();

            string[] queryParts = query.getQueryParts();
            string[] tokens = query.getTokens();


            for (int i = 0; i < queryParts.Length; i++)
            {
                if (tokens[i] == Tokens.Condition || tokens[i] == Tokens.Linker || tokens[i] == Tokens.DefaultLinker)
                {
                    if (tokens[i] == Tokens.DefaultLinker)
                    {
                        condtions.Add(QuerySyntax.DefaultLinker);
                        conditionsTokens.Add(Tokens.Linker);
                    }
                    else
                    {
                        if (tokens[i] == Tokens.Condition && queryParts[i].Substring(0, 1) == "!")
                        {
                            condtions.Add(queryParts[i].Substring(1, queryParts[i].Length - 1));
                        }
                        else
                            condtions.Add(queryParts[i]);

                        conditionsTokens.Add(tokens[i]);

                    }
                }
            }

            query.setConditions(condtions.ToArray());
            query.setLevel(TranslatedLevl.Condition);
            return condtions.ToArray();

        }

        public static string[] columns(Query query)
        {
            if (query.getLevel() < TranslatedLevl.Tokenized) throw new Exception("Run tokenize before condtion.");

            List<string> columns = new List<string>();
            string[] queryParts = query.getQueryParts();
            string[] tokens = query.getTokens();


            for (int i = 0; i < queryParts.Length; i++)
            {
                if (tokens[i] == Tokens.Column && queryParts[i].Substring(0, 1) != "!")
                {
                    if (!columns.Contains(queryParts[i]))
                        columns.Add(queryParts[i]);
                }
                else if (tokens[i] == Tokens.Condition && queryParts[i].Substring(0, 1) != "!")
                {
                    string col = queryParts[i].Split(" ")[0];
                    if (!columns.Contains(col))
                        columns.Add(col);
                }
            }

            if (columns.Count > 0)
                query.setCols(columns.ToArray());
            query.setLevel(TranslatedLevl.Columns);
            return columns.ToArray();

        }

        private static void balancer(Query query)
        {
            if (query.getLevel() < TranslatedLevl.Tokenized) throw new Exception("Run tokenize before condtion.");

            List<string> conditionsTokens = new List<string>();

            List<string> tokens = new List<string>();
            tokens.AddRange(query.getTokens());
            List<string> queryParts = new List<string>();
            queryParts.AddRange(query.getQueryParts());


            for (int i = 0; i < query.getQueryParts().Length; i++)
            {
                bool addOne = false;
                string previousToken = conditionsTokens.Count == 0 ? "no-value" : conditionsTokens[conditionsTokens.Count - 1];
                if (tokens[i] == Tokens.Condition || tokens[i] == Tokens.Linker)
                {
                    if (previousToken == Tokens.Condition && tokens[i] == Tokens.Condition)
                    {
                        conditionsTokens.Add(Tokens.Linker);
                        queryParts.Insert(i, "");
                        tokens.Insert(i, Tokens.DefaultLinker);
                        addOne = true;
                    }
                    conditionsTokens.Add(tokens[i]);
                    if (addOne)
                    {
                        i += 1;
                    }
                }
            }

            query.setQueryParts(queryParts.ToArray());
            query.setTokens(tokens.ToArray());
        }

        public static string[] tokenize(Query query, bool runBalancer)
        {
            string[] queryParts = query.getQueryParts();
            List<string> tokens = new List<string>();
            List<string> newQueryParts = new List<string>();

            for (int i = 0; i < queryParts.Length; i++)
            {
                string value = queryParts[i];
                string nextValue = i + 1 >= queryParts.Length ? "no-value" : queryParts[i + 1];
                string previousValue = i - 1 < 0 ? "no-value" : queryParts[i - 1];
                //string previousToken = i - 1 < 0 ? "no-value" : tokens[i - 1];

                if (i == 0 && !syntax.Contains(nextValue, StringComparer.OrdinalIgnoreCase))
                {
                    tokens.Add(Tokens.TableName);
                    newQueryParts.Add(value);
                }
                else if (syntax.Contains(nextValue, StringComparer.OrdinalIgnoreCase))
                {
                    tokens.Add(Tokens.Condition);
                    string newValue = value;
                    for (int j = i + 1; j < queryParts.Length; j++)
                    {
                        string valueInner = queryParts[j];
                        string previousValueInner = j - 1 < 0 ? "no-value" : queryParts[j - 1];
                        if (!syntax.Contains(valueInner, StringComparer.OrdinalIgnoreCase) && syntax.Contains(previousValueInner, StringComparer.OrdinalIgnoreCase))
                        {
                            newValue += $" {valueInner}";
                            i = j;
                            break;
                        }
                        else
                        {
                            newValue += $" {valueInner}";
                        }
                    }
                    newQueryParts.Add(newValue);
                }
                else if (!syntax.Contains(value, StringComparer.OrdinalIgnoreCase) && !linker.Contains(value, StringComparer.OrdinalIgnoreCase) && !linker.Contains(nextValue, StringComparer.OrdinalIgnoreCase) && value != capsule[0])
                {
                    tokens.Add(Tokens.Column);
                    newQueryParts.Add(value);
                }
                else if (value == capsule[0])
                {
                    tokens.Add(Tokens.Capsule);
                    string capsuleQuery = value;
                    for (int j = i + 1; j < queryParts.Length; j++)
                    {
                        string valueInner = queryParts[j];
                        if (valueInner == capsule[1])
                        {
                            i = j;
                            break;
                        }
                        else
                        {
                            capsuleQuery += $" {valueInner}";
                        }
                    }
                    newQueryParts.Add(capsuleQuery);
                }
                else if (linker.Contains(value, StringComparer.OrdinalIgnoreCase))
                {
                    tokens.Add(Tokens.Linker);
                    newQueryParts.Add(value);
                }
            }

            query.setLevel(TranslatedLevl.Tokenized);
            query.setTokens(tokens.ToArray());
            query.setQueryParts(newQueryParts.ToArray());
            if (runBalancer)
                balancer(query);
            if (tokens[0] == Tokens.TableName)
                query.setTable(newQueryParts[0]);
            return query.getTokens();
        }
    }


}
