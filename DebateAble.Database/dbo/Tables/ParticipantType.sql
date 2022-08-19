CREATE TABLE [dbo].[ParticipantType]
(
	Id int NOT NULL,
	Name nvarchar(50) NOT NULL,
	[CanPost] bit NOT NULL,
	[CanComment] bit NOT NULL,
	CanView bit NOT NULL,
	CONSTRAINT PK_ParticipantType PRIMARY KEY(Id)
)
