CREATE TABLE [dbo].[DebateComment]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	DebateId uniqueidentifier NOT NULL,
	AppUserId uniqueidentifier NOT NULL,
	Comment nvarchar(max) NOT NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_DebateComment PRIMARY KEY(Id),
	CONSTRAINT FK_DebateComment_DebateID FOREIGN KEY(DebateId) REFERENCES Debate(Id),
	CONSTRAINT FK_DebateComment_AppUserId FOREIGN KEY(AppUserID) REFERENCES AppUser(ID)
)
