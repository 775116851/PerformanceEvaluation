GO
CREATE DATABASE PerformanceEvaluationDB
GO
USE PerformanceEvaluationDB
GO
--��Ա������Ϣ��
CREATE TABLE PersonInfo
(
SysNo INT IDENTITY(1,1) PRIMARY KEY,
OrganSysNo INT,
ClassSysNo INT,
[Name] NVARCHAR(32),
BirthDate DATETIME,
EntryDate DATETIME,
OutData DATETIME,
[Status] SMALLINT,
SkillCategory NVARCHAR(32),
ParentPersonSysNo INT,
TelPhone NVARCHAR(32),
IsLogin SMALLINT,
LoginPwd NVARCHAR(32),
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT,
IsAdmin SMALLINT
)
GO
--���Ż�����
CREATE TABLE Organ
(
SysNo INT IDENTITY(1,1) PRIMARY KEY,
OrganId NVARCHAR(32),
OrganType SMALLINT,
FunctionInfo NVARCHAR(100),
OrganName NVARCHAR(32),
PersonNum INT,
AGradScale DECIMAL(9,2),
BGradScale DECIMAL(9,2),
PersonSysNo INT,
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT
)
GO
--��Ч����Ҫ�ر�����Ч������Ա���ã�
CREATE TABLE JXKHYSB
(
SysNo INT IDENTITY(1,1) PRIMARY KEY,
JXId NVARCHAR(32),
JXCategory SMALLINT,
JXInfo NVARCHAR(300),
JXScore DECIMAL(9,2),
JXGrad DECIMAL(9,2),
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT
)
GO
--��Ч���˹�����ÿ��������Ա��Ͻ��Ա��
CREATE TABLE JXKHGSB
(
SysNo INT IDENTITY(1,1) PRIMARY KEY,
ParentPersonSysNo INT,
OrganSysNo INT,
LowerPersonSysNo INT,
LowerClassSysNo INT,
JXCategory SMALLINT,
GradScale DECIMAL(9,2),
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT
)
GO
--��Ч��ϸ��
CREATE TABLE JXMXB
(
SysNo INT IDENTITY(1,1) PRIMARY KEY,
LowerPersonSysNo INT,
JXCategory INT,
JXSysNo INT,
ParentPersonSysNo INT,
JXCycle NVARCHAR(32),
JXScore DECIMAL(9,2),
JXLevel SMALLINT,
JXMXCategory SMALLINT,
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT
)
GO
GO
SELECT * FROM PersonInfo
SELECT * FROM Organ
GO
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10000,10,'������Ӧ��','������Ӧ��',12,40,40,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10001,20,'������Ӧ��A','������Ӧ��A',12,30,30,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10002,20,'������Ӧ��B','������Ӧ��B',12,30,30,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10003,20,'������Ӧ��C','������Ӧ��C',12,30,30,GETDATE(),GETDATE())
GO
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,2,'��',GETDATE(),GETDATE(),0,'һ','88888888','1','11111',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,2,'½',GETDATE(),GETDATE(),0,'��','1','88888888','1','11111',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,2,'�L',GETDATE(),GETDATE(),0,'��','2','88888888','0','11111',GETDATE(),GETDATE(),0)
GO
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('10001','10','���°�����',100,5,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('10002','10','�޹ʿ���',100,20,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('10003','10','�Ͻ��ĵ�',100,10,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('20004','20','���Ա��',100,30,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('20005','20','������',100,10,GETDATE(),GETDATE())
GO
--INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime)
--VALUES(1,1,2,2,20,30,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime)
--VALUES(1,1,3,2,10,50,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime)
--VALUES(2,1,3,2,10,30,GETDATE(),GETDATE())

--UPDATE dbo.Organ SET PersonSysNo = '1' WHERE SysNo IN (1,2)
--UPDATE dbo.PersonInfo SET Status = '0',LoginPwd = '9577C930E002DFE330CEFAFBA8DF82DE'
GO
SELECT * FROM PersonInfo
SELECT * FROM Organ
SELECT * FROM dbo.JXKHYSB
SELECT * FROM JXKHGSB
SELECT * FROM dbo.JXMXB
GO
SELECT '201602' AS JXZQ,b.SysNo,b.Name,b.EntryDate,b.SkillCategory,CASE WHEN c.SysNo IS NULL THEN 0 ELSE 1 END AS IsPF,c.JXScore,c.JXLevel,c.SysNo AS JXMXSysNo 
FROM dbo.JXKHGSB a WITH (NOLOCK) INNER JOIN dbo.PersonInfo b WITH (NOLOCK) ON a.LowerPersonSysNo = b.SysNo AND b.Status = '0'
LEFT JOIN dbo.JXMXB c WITH (NOLOCK) ON b.SysNo = c.LowerPersonSysNo AND a.ParentPersonSysNo = c.ParentPersonSysNo AND c.JXMXCategory = '20' AND c.JXCycle = '201602'
WHERE 1=1 AND a.ParentPersonSysNo = '1'
AND b.Name LIKE '%�L%'
AND b.SysNo NOT IN (SELECT DISTINCT LowerPersonSysNo FROM dbo.JXMXB m WITH (NOLOCK) WHERE m.ParentPersonSysNo = '1' AND m.JXMXCategory = '20' AND m.JXCycle = '201602')
ORDER BY b.EntryDate DESC
GO
SELECT b.SysNo,b.JXId,b.JXCategory,b.JXInfo,b.JXScore,b.JXGrad,a.OrganSysNo,c.JXScore AS JXMXScore,c.SysNo AS JXMXSysNo
FROM dbo.JXKHGSB a WITH (NOLOCK) INNER JOIN dbo.JXKHYSB b WITH (NOLOCK) ON a.JXCategory = b.JXCategory
LEFT JOIN dbo.JXMXB c WITH (NOLOCK) ON c.ParentPersonSysNo = a.ParentPersonSysNo AND c.LowerPersonSysNo = a.LowerPersonSysNo AND b.SysNo = c.JXSysNo AND c.JXMXCategory = '10' AND c.JXCycle = '201602'
WHERE 1=1 AND a.ParentPersonSysNo = '1' AND a.LowerPersonSysNo = '3'

GO
SELECT CAST(JXCategory AS VARCHAR(2)) + RIGHT('00' + CAST(11 AS VARCHAR), 3),SysNo,JXId FROM dbo.JXKHYSB
GO

UPDATE JXKHYSB SET JXInfo = '���Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա�����Ա��' WHERE SysNo = '4'
UPDATE JXKHYSB SET JXInfo = '������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������������' WHERE SysNo = '5'
GO


30  100  60   18
50  100  40   20
20  100  50   10


