SELECT 
	Distinct SO.Name
FROM 
	sysobjects SO (NOLOCK)
	INNER JOIN syscomments SC (NOLOCK) on 
		SO.Id = SC.ID
		AND 
		SO.Type = 'P'
		AND 
		SC.Text LIKE @ToFind
ORDER BY 
	SO.Name