namespace PowerMessenger.Infrastructure.Persistence.NpgSetting;

public static class NpgFunctionsMigration
{
    public static IEnumerable<string> CreateFunctions()
    {
        var pathSqlDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sql");
        var filesSqlPath = Directory.GetFiles(pathSqlDirectory);
        
        foreach (var sqlPath in filesSqlPath)
        {
            if (!File.Exists(sqlPath)) continue;
            
            var sql = File.ReadAllText(sqlPath);
            
            if (!sql.Contains("FUNCTION", StringComparison.OrdinalIgnoreCase)) 
                continue;
            
            yield return sql;
        }
    }
    
    public static IEnumerable<string> DeleteFunction()
    {
        var pathSqlDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sql");
        var filesSqlPath = Directory.GetFiles(pathSqlDirectory);

        foreach (var sqlPath in filesSqlPath)
        {
            if (!File.Exists(sqlPath)) 
                continue;

            var sql = File.ReadAllText(sqlPath);
            
            if (!sql.Contains("FUNCTION", StringComparison.OrdinalIgnoreCase)) 
                continue;
            
            var functionName = GetFunctionNameFromSql(sql);

            yield return $"DROP FUNCTION {functionName}";
        }
    }
    
    
    private static string GetFunctionNameFromSql(string sql)
    {
        const string functionKeyword = "FUNCTION";
        var startIndex = sql.IndexOf(functionKeyword, StringComparison.Ordinal) + functionKeyword.Length;
        if (startIndex < functionKeyword.Length)
        {
            throw new ArgumentException("Invalid SQL content: Function keyword not found.");
        }
        
        var openingBracketIndex = sql.IndexOf('(', startIndex);

        if (openingBracketIndex == -1)
        {
            throw new ArgumentException("Invalid SQL content: Opening parenthesis not found.");
        }
        
        var functionName = sql.Substring(startIndex, openingBracketIndex - startIndex).Trim();

        return functionName;
    }
}