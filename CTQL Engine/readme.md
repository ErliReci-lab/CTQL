table col1 col3 col4 = "test" and ( col5 ~ "abc" or col5 ~ "cba" ) col6 col7 = "dce"

tranform to:

org:
table_name column column condition condition condition linker capsule_start capsule_inner capsule_inner capsule_inner capsule_inner capsule_inner capsule_inner capsule_inner capsule_end column linker condition condition condition
test:
table_name column column condition condition condition linker capsule_start capsule_inner capsule_inner capsule_inner capsule_inner capsule_inner capsule_inner capsule_inner capsule_end column condition condition condition


table col1 col3 col4 = "test" and col5 ~ "abc" or col5 ~ "cba" col6 col7 = "dce"

tranform to:

org:
table_name column column condition condition condition linker condition condition condition linker condition condition condition column linker condition condition condition
test:
table_name column column condition condition condition linker condition condition condition linker condition condition condition column condition condition condition

syntax = ["=", "~", ">", "<", "<=", ">=", "!=", "!~"]
linkers = ["and", "or"]

index   value               tag             logic   
----------------------------------------------------
0       table               table_name      index = 0
1       col1                column          if value not in syntax and value not in linkers and (next value) not in linkers
2       col3                column          if value not in syntax and value not in linkers and (next value) not in linkers
3       col4                condition       if (next value) in syntax
4       =                   condition       if value in syntax
5       "test"              condition       if (previous value) in syntax
6       and                 linker          if value in linkers
7       (                   capsule_start   if value = "("
8       col5                capsule_inner   if (previous tag) = capsule_inner
9       ~                   capsule_inner   if (previous tag) = capsule_inner
10      "abc"               capsule_inner   if (previous tag) = capsule_inner
11      or                  capsule_inner   if (previous tag) = capsule_inner
12      col5                capsule_inner   if (previous tag) = capsule_inner
13      ~                   capsule_inner   if (previous tag) = capsule_inner
14      "cba"               capsule_inner   if (previous tag) = capsule_inner
15      )                   capsule_end     if value = "("
16      col6                column          if value not in syntax and value not in linkers and (next value) not in linkers
        <default_linker>    linker          this will be added if a column is followed by a condition
17      col7                condition       if (next value) in syntax
18      =                   condition       if value in syntax
19      "dce"               condition       if (previous value) in syntax