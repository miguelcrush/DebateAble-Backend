CREATE TABLE [dbo].[AppUser]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	Email nvarchar(255) NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnDate datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_AppUser PRIMARY KEY(Id),
	CONSTRAINT UX_AppUser UNIQUE(Email)
)
