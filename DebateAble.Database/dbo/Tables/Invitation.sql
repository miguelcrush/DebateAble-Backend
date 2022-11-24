CREATE TABLE [dbo].[Invitation]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	InviterAppUserId uniqueidentifier NOT NULL,
	InviteeAppUserId uniqueidentifier NOT NULL,
	InvitationSentUtc datetime2 NULL,
	InvitationToken nvarchar(1024) NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_Invitation PRIMARY KEY(Id),
	CONSTRAINT FK_Invitation_InviterAppUserId FOREIGN KEY(InviterAppUserId) REFERENCES AppUser(Id),
	CONSTRAINT FK_Invitation_InviteeAppUserId FOREIGN KEY(InviteeAppUserId) REFERENCES AppUSer(Id)
)
