using CTQL_Engine;

EngineProxy engine = new EngineProxy();
engine.log = true;

Console.WriteLine(engine.CTQL_SQL("table_data col1 col3 col4 = 'test' and col5 ~ 'abc' or col5 ~ 'cba' col6 col7 = 'dce'"));

Console.WriteLine("========================================================================");

Console.WriteLine(engine.CTQL_SQL("!col4 = 'test' and !col5 ~ 'abc'"));

Console.WriteLine("========================================================================");

Console.WriteLine(engine.CTQL_SQL("table_data !col4 = 'test' and !col5 ~ 'abc'"));

Console.WriteLine("========================================================================");

Console.WriteLine(engine.CTQL_SQL("table_data col1 col2 col3"));

Console.ReadLine();