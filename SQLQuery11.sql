--INNER JOIN
SELECT un.GroupId, un.Name, un.Email, un.Status, g.GroupName FROM dbo.[UserName] un 
INNER JOIN dbo.[Group] g 
ON un.GroupId = g.ID
--GROUP BY 
SELECT us.ID, us.Name, us.GroupId, g.Status FROM dbo.[UserName] us
INNER JOIN dbo.[Group] g 
ON us.GroupId = g.ID

GROUP BY us.ID, us.Name, us.GroupId, g.Status 


HAVING g.Status = 'true'; 

--LEFT JOIN
SELECT us.GroupId, us.Name, us.Email, g.Status, g.GroupName FROM dbo.[UserName] us 

LEFT JOIN dbo.[Group] g
ON us.GroupId = g.ID
WHERE g.GroupName = 'admin';

--RINGH JOID

--FULL JOIN