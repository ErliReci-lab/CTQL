using CTQL_Engine;

Query query = new($"table col1 col3 col4 = 'test' and col5 ~ 'abc' or col5 ~ 'cba' col6 col7 = 'dce'");
Console.WriteLine(query.getCTQL());

string[] token = Lexer.tokenize(query);
Console.WriteLine(String.Join(" ", token));

string[] conditions = Lexer.condtion(query);
Console.WriteLine(String.Join(" ", conditions));

Console.ReadLine();