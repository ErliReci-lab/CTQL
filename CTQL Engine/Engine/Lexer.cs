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
                if (tokens[i] == Tokens.Condition || tokens[i] == Tokens.Linker)
                {
                    condtions.Add(queryParts[i]);
                    conditionsTokens.Add(tokens[i]);
                }
            }

            return condtions.ToArray();

        }

        public static string[] tokenize(Query query)
        {
            List<string> tokens = new List<string>();
            List<string> queryParts = new List<string>();
            queryParts.AddRange(query.getQueryParts());
            int length = query.getQueryParts().Length;
            bool first = true;

            for (int i = 0; i < length; i++)
            {
                string value = queryParts[i];
                string nextValue = i + 1 >= length ? "no-value" : queryParts[i + 1];
                string previousValue = i - 1 < 0 ? "no-value" : queryParts[i - 1];
                string previousToken = i - 1 < 0 ? "no-value" : tokens[i - 1];

                if (i == 0)
                {
                    tokens.Add(Tokens.TableName);
                }
                else if (syntax.Contains(nextValue) || syntax.Contains(value) || syntax.Contains(previousValue))
                {
                    if (previousToken == Tokens.Condition)
                    {
                        if (first) 
                        {
                            first = false;
                        }
                        else
                        {
                            queryParts.Insert(i, QuerySyntax.DefaultLinker);
                            tokens.Add(Tokens.Linker);
                        }
                    }
                    tokens.Add(Tokens.Condition);
                }
                else if (!syntax.Contains(value) && !linker.Contains(value) && !linker.Contains(nextValue) && value != capsule[0])
                {
                    tokens.Add(Tokens.Column);
                }
                else if (value == capsule[0])
                {
                    tokens.Add(Tokens.CapsuleStart);
                    for (int j = i; j < length; j++)
                    {
                        string valueInner = queryParts[j];
                        if (valueInner == capsule[1])
                        {
                            tokens.Add(Tokens.CapsuleEnd);
                            i = j;
                        }
                        else
                        {
                            tokens.Add(Tokens.CapsuleInner);
                        }
                    }
                }
                else if (linker.Contains(value))
                {
                    tokens.Add(Tokens.Linker);
                }
            }

            query.setTokens(tokens.ToArray());
            query.setQueryParts(queryParts.ToArray());
            query.setLevel(TranslatedLevl.Tokenized);
            return tokens.ToArray();
        }
    }


}
