CREATE TABLE [dbo].[DebatePost]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	DebateId uniqueidentifier NOT NULL,
	AppUserID uniqueidentifier NOT NULL,
	Post nvarchar(max) NOT NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_DebatePost PRIMARY KEY(ID),
	CONSTRAINT FK_DebatePost_DebateId FOREIGN KEY(DebateId) REFERENCES Debate(Id),
	CONSTRAINT FK_DebatePost_AppUserId FOREIGN KEY(AppUserId) REFERENCES AppUser(Id)
)
