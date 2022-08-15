CREATE TABLE [dbo].[Debate]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	[CreatedByAppUserId] uniqueidentifier NOT NULL,
	Title nvarchar(1024) NOT NULL,
	Description nvarchar(MAX) NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_Debate PRIMARY KEY(Id),
	CONSTRAINT FK_Debate_AppUserId FOREIGN KEY([CreatedByAppUserId]) REFERENCES AppUser(Id)
)
