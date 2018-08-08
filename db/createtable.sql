BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS `Skill` (
	'Id'	INTEGER PRIMARY KEY AUTOINCREMENT,
	'Name'	text NOT NULL
);
INSERT INTO 'Skill' (Id,Name) VALUES (1,'SQL');
INSERT INTO 'Skill' (Id,Name) VALUES (2,'JavaScript');
INSERT INTO 'Skill' (Id,Name) VALUES (3,'C#');
INSERT INTO 'Skill' (Id,Name) VALUES (4,'Java');
INSERT INTO 'Skill' (Id,Name) VALUES (5,'Python');
CREATE TABLE IF NOT EXISTS 'CandidateSkill' (
	'CandidateID'	INTEGER NOT NULL,
	'SkillID'	INTEGER NOT NULL,
	PRIMARY KEY('CandidateID','SkillID')
);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (1,2);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (1,1);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (2,2);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (2,3);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (4,4);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (4,5);
INSERT INTO 'CandidateSkill' (CandidateID,SkillID) VALUES (4,3);
CREATE TABLE IF NOT EXISTS 'Candidate' (
	'Id'	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	'FirstName'	text NOT NULL,
	'LastName'	text NOT NULL,
	'EnteredDate'	Datetime NOT NULL
);
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (1,'Jon','Snow','2000-01-01 00:00:00');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (2,'Daenerys','Tangaryen','2000-01-01 00:00:00');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (3,'Cersei','Lannister','2000-01-01 00:00:00');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (4,'Sansa','Stark','2000-01-01 00:00:00');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (5,'Arya','Stark','2000-01-01 00:00:00');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (6,'Tyrion','Lannister','2000-01-01 00:00:00');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (7,'New','Candidate','2018-08-07 07:31:59.9055334');
INSERT INTO 'Candidate' (Id,FirstName,LastName,EnteredDate) VALUES (8,'Nikolay','Mikheev','2018-08-07 12:19:28.3797349');
COMMIT;
