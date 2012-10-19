SELECT name
FROM sys.procedures
WHERE [is_ms_shipped] = 0
ORDER BY [name];