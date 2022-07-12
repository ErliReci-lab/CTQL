# Description
Create straightforward filters, then turn them into intricate SQL queries.

# Rules

1) The table name will always be the first word, unless a syntax { "=", "\~", ">", "<", "<=", ">=", "!=", "!~" } comes after it. . 
2) If you wish to select a column, just add the name of the column in the query. Even columns that are included in the condition can be selected. If you wish to hide a column, you should add ! in front of the column.

## Table of conversion

SQL | CTQL
----|-----
Col1 LIKE "%name%" | Col1 ~ "name"
Col1 NOT LIKE "%name%" | Col1 !~ "name"
Col1 = "name" | Col1 = "name"
Col1 <> "name" | Col1 != "name"
Col1 = "name" and Col2 <> "a" | Col1 = "name" and Col2 != "a"
Col1 = "name" or Col2 <> "a" | Col1 = "name" or Col2 != "a"
Col1 in ("name", "a") | Col1 ~ ("name", "a")


# Examples

## Example 1
- CTQL 
    ```
    table_data col1 col3 col4 = 'test' and col5 ~ 'abc' or col5 ~ 'cba' col6 col7 = 'dce'
    ```
- SQL 
    ```sql
    SELECT col1, col3, col4, col5, col6, col7 
    FROM table_data
    WHERE col4 = 'test' AND col5 LIKE 'abc' OR col5 LIKE 'cba' AND col7 = 'dce'
    ```
## Example 2
- CTQL 
    ``` 
    !col4 = 'test' and !col5 ~ 'abc'
    ```
- SQL 
    ```sql
    SELECT * 
    FROM table 
    WHERE col4 = 'test' AND col5 LIKE 'abc'
    ```
## Example 3
- CTQL 
    ``` 
    table_data !col4 = 'test' and !col5 ~ 'abc'
    ```
- SQL 
    ```sql
    SELECT * 
    FROM table_data 
    WHERE col4 = 'test' AND col5 LIKE 'abc'
    ```

## Example 4 
- CTQL 
    ``` 
    table_data col1 col2 col3
    ```
- SQL 
    ```sql
    SELECT col1, col2, col3 
    FROM table_data
    ```
