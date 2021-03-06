GO
CREATE DATABASE PerformanceEvaluationDB
GO
USE PerformanceEvaluationDB
GO
--人员基本信息表
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
--人员类型表
CREATE TABLE PersonType
(
SysNo INT IDENTITY(1,1) PRIMARY KEY,
TypeName NVARCHAR(32),
[Status] SMALLINT,
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT
)
GO
--部门机构表
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
--绩效考核要素表（供绩效管理人员配置）
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
--绩效考核归属表（每个管理人员所辖人员）
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
--绩效明细表
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
[Status] SMALLINT,
CreateTime DATETIME,
LastUpdateTime DATETIME,
CreateUserSysNo INT,
LastUpdateUserSysNo INT
)
GO
--无用
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='Birthday')
BEGIN
ALTER TABLE PersonInfo ADD Birthday DATETIME
END
--手机号
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='MobilePhone')
BEGIN
ALTER TABLE PersonInfo ADD MobilePhone NVARCHAR(11) 
END
--QQ
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='QQ')
BEGIN
ALTER TABLE PersonInfo ADD QQ NVARCHAR(20) 
END
--邮箱
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='Email')
BEGIN
ALTER TABLE PersonInfo ADD Email NVARCHAR(20) 
END
--性别(1男 2女)
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='Gender')
BEGIN
ALTER TABLE PersonInfo ADD Gender SMALLINT
END
--备注
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='Note')
BEGIN
ALTER TABLE PersonInfo ADD Note NVARCHAR(200)
END
--人员类型
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='PersonTypeSysNo')
BEGIN
ALTER TABLE PersonInfo ADD PersonTypeSysNo INT
END
--备用字段一
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='BYZD1')
BEGIN
ALTER TABLE PersonInfo ADD BYZD1 NVARCHAR(200)
END
--备用字段二
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='BYZD2')
BEGIN
ALTER TABLE PersonInfo ADD BYZD2 NVARCHAR(200)
END
--备用字段三
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='BYZD3')
BEGIN
ALTER TABLE PersonInfo ADD BYZD3 NVARCHAR(200)
END
--记录类型(1手工填写 2系统填写)
IF NOT EXISTS(select * from syscolumns where id=object_id('JXKHGSB') and name='RecordType')
BEGIN
ALTER TABLE JXKHGSB ADD RecordType SMALLINT
END
--记录类型(1手工填写 2系统填写)
IF NOT EXISTS(select * from syscolumns where id=object_id('JXMXB') and name='RecordType')
BEGIN
ALTER TABLE JXMXB ADD RecordType SMALLINT
END
GO
SELECT * FROM PersonInfo
SELECT * FROM Organ
SELECT * FROM PersonType
GO
--TRUNCATE TABLE PersonType
--INSERT INTO PersonType(TypeName,Status,CreateTime,LastUpdateTime)
--VALUES('中心主要负责人',0,GETDATE(),GETDATE())
--INSERT INTO PersonType(TypeName,Status,CreateTime,LastUpdateTime)
--VALUES('中心主要非负责人',0,GETDATE(),GETDATE())
--INSERT INTO PersonType(TypeName,Status,CreateTime,LastUpdateTime)
--VALUES('二级部主要负责人',0,GETDATE(),GETDATE())
--INSERT INTO PersonType(TypeName,Status,CreateTime,LastUpdateTime)
--VALUES('二级部主要非负责人',0,GETDATE(),GETDATE())
--INSERT INTO PersonType(TypeName,Status,CreateTime,LastUpdateTime)
--VALUES('室经理',0,GETDATE(),GETDATE())
--INSERT INTO PersonType(TypeName,Status,CreateTime,LastUpdateTime)
--VALUES('普通员工',0,GETDATE(),GETDATE())
--TRUNCATE TABLE dbo.Organ
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10000,10,'互联网应用部','互联网应用部',5,40,30,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(20000,10,'电子商务部','电子商务部',1,40,40,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10001,20,'互联网应用A','互联网应用A',6,30,30,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(10002,20,'互联网应用B','互联网应用B',6,30,30,GETDATE(),GETDATE())
--INSERT INTO dbo.Organ(OrganId,OrganType,FunctionInfo,OrganName,PersonNum,AGradScale,BGradScale,CreateTime,LastUpdateTime)
--VALUES(20001,20,'电子商务部A','电子商务部A',2,30,30,GETDATE(),GETDATE())
--GO
--TRUNCATE TABLE dbo.PersonInfo
--INSERT INTO dbo.PersonInfo(Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES('公司老大',GETDATE(),GETDATE(),0,'公司老大','99999','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),1)
--INSERT INTO dbo.PersonInfo(Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES('绩效管理员',GETDATE(),GETDATE(),0,'绩效管理员','-99','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),1)
--INSERT INTO dbo.PersonInfo(OrganSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,'互联网部门经理',GETDATE(),GETDATE(),0,'互联网部门经理','1','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(2,'电子商务部门经理',GETDATE(),GETDATE(),0,'电子商务部门经理','1','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,3,'互联经理A',GETDATE(),GETDATE(),0,'互联经理A','3','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,4,'互联经理B',GETDATE(),GETDATE(),0,'互联经理B','3','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(2,5,'电子经理A',GETDATE(),GETDATE(),0,'电子经理A','4','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,3,'互联经A员工',GETDATE(),GETDATE(),0,'互联经A员工','5','88888888','1','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,3,'互联经A员工2',GETDATE(),GETDATE(),0,'互联经A员工2','5','88888888','0','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--INSERT INTO dbo.PersonInfo(OrganSysNo,ClassSysNo,Name,BirthDate,EntryDate,Status,SkillCategory,ParentPersonSysNo,TelPhone,IsLogin,LoginPwd,CreateTime,LastUpdateTime,IsAdmin)
--VALUES(1,4,'互联经B员工',GETDATE(),GETDATE(),0,'互联经B员工','6','88888888','0','9577C930E002DFE330CEFAFBA8DF82DE',GETDATE(),GETDATE(),0)
--GO
--TRUNCATE TABLE dbo.JXKHYSB
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('10001','10','上下班打卡情况',100,20,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('10002','10','无故旷工',100,50,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('10003','10','上进心等',100,30,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('20004','20','监管员工',100,60,GETDATE(),GETDATE())
--INSERT INTO dbo.JXKHYSB(JXId,JXCategory,JXInfo,JXScore,JXGrad,CreateTime,LastUpdateTime)
--VALUES('20005','20','请假情况',100,40,GETDATE(),GETDATE())
--GO
--UPDATE dbo.Organ SET PersonSysNo = '3' WHERE SysNo = '1'
--UPDATE dbo.Organ SET PersonSysNo = '4' WHERE SysNo = '2'
--UPDATE dbo.Organ SET PersonSysNo = '5' WHERE SysNo = '3'
--UPDATE dbo.Organ SET PersonSysNo = '6' WHERE SysNo = '4'
--UPDATE dbo.Organ SET PersonSysNo = '7' WHERE SysNo = '5'
--GO
TRUNCATE TABLE dbo.JXKHGSB
--公司老大-部门经理及员工
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(1,1,3,20,100,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(1,2,4,20,100,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(1,1,9,10,10,GETDATE(),GETDATE(),1)
--部门经理-经理
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,5,3,10,100,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,6,4,10,100,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(4,2,7,5,10,100,GETDATE(),GETDATE(),1)
--部门经理-员工
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,8,3,10,30,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,8,3,10,50,GETDATE(),GETDATE(),2)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,9,3,10,30,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,9,3,10,20,GETDATE(),GETDATE(),2)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(3,1,10,4,10,60,GETDATE(),GETDATE(),1)
--经理-员工
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(5,1,8,3,10,20,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(5,1,9,3,10,20,GETDATE(),GETDATE(),1)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(5,1,9,3,10,20,GETDATE(),GETDATE(),2)
INSERT INTO dbo.JXKHGSB(ParentPersonSysNo,OrganSysNo,LowerPersonSysNo,LowerClassSysNo,JXCategory,GradScale,CreateTime,LastUpdateTime,RecordType)
VALUES(6,1,10,4,10,40,GETDATE(),GETDATE(),1)
--GO
--TRUNCATE TABLE dbo.JXMXB
GO
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
AND b.Name LIKE '%風%'
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

UPDATE JXKHYSB SET JXInfo = '监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工监管员工' WHERE SysNo = '4'
UPDATE JXKHYSB SET JXInfo = '请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况请假情况' WHERE SysNo = '5'
GO
--UPDATE JXMXB SET Status = '0'
SELECT * FROM JXMXB

30  100  60   18
50  100  40   20
20  100  50   10

SELECT * FROM JXMXB
SELECT * FROM JXKHGSB
GO
SELECT * FROM JXMXB WHERE JXMXCategory = '20' AND Status = '0'
SELECT * FROM JXKHGSB 
GO
SELECT a.LowerPersonSysNo,MAX(a.JXCategory) AS JXCategory,MAX(a.ParentPersonSysNo) AS ParentPersonSysNo,MAX(a.JXCycle) AS JXCycle,SUM(a.JXScore * b.GradScale/100) AS TotalScore 
FROM JXMXB a INNER JOIN JXKHGSB b ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.JXMXCategory = '20' AND a.Status = '0' AND a.JXCycle = '201602'
WHERE 1=1
GROUP BY a.LowerPersonSysNo
GO
--UPDATE JXMXB SET Status = '-1' WHERE JXMXCategory = '99' AND JXCycle = '201602'
GO
SELECT * FROM PersonInfo
SELECT * FROM Organ
SELECT * FROM JXMXB
SELECT * FROM JXKHGSB
SELECT * FROM JXKHYSB
GO
SELECT a.SysNo, a.PersonSysNo,a.PersonNum,a.AGradScale,a.BGradScale,b.Name FROM Organ a LEFT JOIN PersonInfo b ON a.PersonSysNo = b.SysNo WHERE OrganType = '10'
SELECT ISNULL(SUM(CASE WHEN b.JXLevel = 5 THEN 1 ELSE 0 END),0) AS ACount,ISNULL(SUM(CASE WHEN b.JXLevel = 2 THEN 1 ELSE 0 END),0) AS BCount 
FROM JXKHGSB a INNER JOIN JXMXB b ON a.LowerPersonSysNo = b.LowerPersonSysNo AND b.Status = '0' AND b.JXMXCategory = '99' AND b.JXCycle = '201602'
WHERE 1=1 AND a.OrganSysNo = '1'

SELECT * FROM PersonInfo
GO
--UPDATE PersonInfo SET ParentPersonSysNo = '99999',IsAdmin='1' WHERE SysNo = '1'
--UPDATE PersonInfo SET LoginPwd = '9577C930E002DFE330CEFAFBA8DF82DE'
GO
SELECT * FROM dbo.PersonInfo
SELECT * FROM dbo.Organ
SELECT * FROM dbo.JXKHYSB
SELECT * FROM dbo.JXKHGSB
SELECT * FROM dbo.JXMXB
GO
SELECT * FROM dbo.JXMXB WHERE JXMXCategory = '99' AND Status = '0' AND JXCycle = '201602'
GO
SELECT DISTINCT c.SysNo,b.OrganSysNo,b.LowerClassSysNo AS ClassSysNo,a.JXCycle,a.JXScore,a.JXLevel,c.Name,d.OrganName,e.FunctionInfo,c.EntryDate
FROM dbo.JXMXB a INNER JOIN dbo.JXKHGSB b ON a.LowerPersonSysNo = b.LowerPersonSysNo AND a.JXMXCategory = '99' AND a.Status = '0'
INNER JOIN dbo.PersonInfo c ON a.LowerPersonSysNo = c.SysNo
LEFT JOIN dbo.Organ d ON b.OrganSysNo = d.SysNo
LEFT JOIN dbo.Organ e ON b.LowerClassSysNo = e.SysNo
WHERE 1=1 AND (b.ParentPersonSysNo = '2' OR b.LowerPersonSysNo='2') AND a.JXCycle = '201602' AND a.JXLevel = '5' AND c.Name LIKE '%風%' AND b.OrganSysNo = '1' AND b.LowerClassSysNo = '2' AND a.JXCycle LIKE '2016%'
ORDER BY a.JXCycle DESC
GO
SELECT DISTINCT b.SysNo,b.OrganSysNo,b.ClassSysNo,a.JXCycle,a.JXScore,a.JXLevel,b.Name,d.OrganName,e.FunctionInfo,b.EntryDate
FROM dbo.JXMXB a INNER JOIN dbo.PersonInfo b ON a.LowerPersonSysNo = b.SysNo AND a.JXMXCategory = '99' AND a.Status = '0'
LEFT JOIN dbo.Organ d ON b.OrganSysNo = d.SysNo
LEFT JOIN dbo.Organ e ON b.ClassSysNo = e.SysNo
WHERE 1=1 AND a.JXCycle = '201602' AND a.JXLevel = '5' AND b.Name LIKE '%風%' AND b.OrganSysNo = '1' AND b.ClassSysNo = '2' AND a.JXCycle LIKE '2016%'
ORDER BY a.JXCycle DESC
GO
SELECT SysNo AS [key],OrganName AS value FROM dbo.Organ WHERE OrganType = '10' ORDER BY OrganId ASC
SELECT SysNo AS [key],FunctionInfo AS value FROM dbo.Organ WHERE OrganType = '20' AND OrganId LIKE '10%' ORDER BY OrganId ASC

ddlOrgan=1&ddlClass=-9999&ddlYY=2016&ddlMM=2&txtPersonName=aaa&ddlLevel%24ddlEnum=5


SELECT * FROM dbo.Organ WITH (NOLOCK) WHERE 1=1 AND OrganType = '10' ORDER BY OrganId ASC
GO
GO
SELECT DISTINCT ConfigValue FROM dbo.Sys_Configuration WITH (NOLOCK) WHERE ConfigKey = 'LevelRangeScore'
GO
SELECT * FROM PersonInfo
SELECT * FROM JXMXB


SELECT a.* FROM PersonType a WITH (NOLOCK) WHERE 1=1 AND a.Status = '0' ORDER BY SysNo ASC
GO
SELECT a.SysNo,a.OrganSysNo,a.ClassSysNo,a.Name,a.EntryDate,a.Status,a.IsLogin,a.PersonTypeSysNo,c.OrganName,d.FunctionInfo,b.TypeName 
FROM PersonInfo a WITH (NOLOCK) LEFT JOIN PersonType b WITH (NOLOCK) ON a.PersonTypeSysNo = b.SysNo
LEFT JOIN Organ c WITH (NOLOCK) ON a.OrganSysNo = c.SysNo
LEFT JOIN Organ d WITH (NOLOCK) ON a.ClassSysNo = d.SysNo
WHERE 1=1 AND ISNULL(a.IsAdmin,0) = 0 AND a.OrganSysNo = '1' AND a.ClassSysNo = '3' AND a.Status = '0' AND a.Name LIKE '%互联%' AND a.PersonTypeSysNo = ''
ORDER BY a.SysNo ASC

SELECT * FROM PersonType
SELECT * FROM PersonInfo
--UPDATE PersonInfo SET PersonTypeSysNo = '6' WHERE SysNo IN ('8','9','10')
GO
--加权汇总（正常数据汇总）
SELECT a.LowerPersonSysNo,MAX(a.JXCategory) AS JXCategory,MAX(a.ParentPersonSysNo) AS ParentPersonSysNo,MAX(a.JXCycle) AS JXCycle,SUM(a.JXScore * b.GradScale/100) AS TotalScore
FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.RecordType = b.RecordType AND a.JXMXCategory = '20' AND a.Status = '0' AND a.JXCycle = '201603' AND b.RecordType = '1'
WHERE 1=1 AND a.LowerPersonSysNo NOT IN (SELECT DISTINCT LowerPersonSysNo FROM JXKHGSB WHERE RecordType = '2')
GROUP BY a.LowerPersonSysNo

DELETE FROM JXMXB WHERE JXCycle = '201603' AND JXMXCategory = '20' AND RecordType = '2';
SELECT b.ParentPersonSysNo,b.LowerPersonSysNo,b.JXCategory,'201603' AS JXCycle,a.JXScore,a.JXLevel,a.JXLevel,'20' AS JXMXCategory
FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.LowerPersonSysNo = b.ParentPersonSysNo AND a.JXMXCategory = '99' AND a.Status = '0' AND a.JXCycle = '201603' AND b.RecordType = '2'


SELECT * FROM JXMXB WHERE JXMXCategory = '20' AND JXCycle = '201603'
SELECT * FROM JXMXB WHERE JXMXCategory = '99' AND JXCycle = '201603'
SELECT * FROM JXKHGSB
SELECT * FROM JXMXB WHERE JXCycle = '201603' AND JXMXCategory IN ('20','99') AND LowerPersonSysNo = '7'
GO
DELETE JXMXB WHERE JXCycle = '201603' AND JXMXCategory = '20'
DELETE JXMXB WHERE SysNo IN ('274','275','276')

SELECT a.LowerPersonSysNo,MAX(a.JXCategory) AS JXCategory,MAX(a.ParentPersonSysNo) AS ParentPersonSysNo,MAX(a.JXCycle) AS JXCycle,SUM(a.JXScore * b.GradScale/100) AS TotalScore
FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.JXMXCategory = '20' AND a.Status = '0' AND a.JXCycle = '201603' AND b.RecordType = '2'
WHERE 1=1 
GROUP BY a.LowerPersonSysNo


SELECT * FROM JXMXB WHERE JXMXCategory = '20' AND JXCycle = '201603' AND LowerPersonSysNo IN ('8','9')
SELECT * FROM JXKHGSB WHERE LowerPersonSysNo IN ('8','9')
SELECT * FROM JXKHGSB WHERE LowerPersonSysNo ='8'
SELECT * FROM JXKHGSB WHERE LowerPersonSysNo ='9'

SELECT a.LowerPersonSysNo,MAX(a.JXCategory) AS JXCategory,MAX(a.ParentPersonSysNo) AS ParentPersonSysNo,MAX(a.JXCycle) AS JXCycle,SUM(a.JXScore * b.GradScale/100) AS TotalScore
FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.RecordType = b.RecordType AND a.JXMXCategory = '20' AND a.Status = '0' AND a.JXCycle = '201603'
WHERE 1=1 AND a.LowerPersonSysNo IN (SELECT DISTINCT LowerPersonSysNo FROM JXKHGSB WHERE RecordType = '2')
GROUP BY a.LowerPersonSysNo

SELECT a.LowerPersonSysNo,a.JXCategory,a.ParentPersonSysNo,a.JXCycle,a.JXScore * b.GradScale/100
FROM JXMXB a WITH (NOLOCK) INNER JOIN JXKHGSB b WITH (NOLOCK) ON a.ParentPersonSysNo = b.ParentPersonSysNo AND a.LowerPersonSysNo = b.LowerPersonSysNo AND a.RecordType = b.RecordType AND a.JXMXCategory = '20' AND a.Status = '0' AND a.JXCycle = '201603'
WHERE 1=1 AND a.LowerPersonSysNo IN (SELECT DISTINCT LowerPersonSysNo FROM JXKHGSB WHERE RecordType = '2')

GO
--20160329修改如下
--修改OutData为OutDate
--转正日期
IF NOT EXISTS(select * from syscolumns where id=object_id('PersonInfo') and name='PositiveDate')
BEGIN
ALTER TABLE PersonInfo ADD PositiveDate DATETIME 
END