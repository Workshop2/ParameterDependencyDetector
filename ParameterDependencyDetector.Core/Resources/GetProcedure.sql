SELECT
	M.Definition
FROM sys.sql_modules as M INNER JOIN sys.objects as O 
ON M.object_id = O.object_id 
WHERE O.type = 'P' AND [name] = @ProcedureName