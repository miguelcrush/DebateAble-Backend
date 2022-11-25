CREATE TABLE [dbo].[AppUser]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	Email nvarchar(255) NULL,
	FirstName nvarchar(50) NULL,
	LastName nvarchar(50) NULL,
	[Invited] BIT NOT NULL DEFAULT 0,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	[ModifiedOnUtc] datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_AppUser PRIMARY KEY(Id),
	CONSTRAINT UX_AppUser UNIQUE(Email)
)
