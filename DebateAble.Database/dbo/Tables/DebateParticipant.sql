CREATE TABLE [dbo].[DebateParticipant]
(
	Id uniqueidentifier NOT NULL DEFAULT(newsequentialid()),
	DebateId uniqueidentifier NOT NULL,
	AppUserId uniqueidentifier NOT NULL,
	ParticipantTypeId int NOT NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnUtc datetime2 NOT NULL DEFAULT(getutcdate())
	CONSTRAINT PK_DebateParticipant_AppUserID PRIMARY KEY(Id),
	CONSTRAINT FK_DebateParticipant_DebateId FOREIGN KEY(DebateId) REFERENCES Debate(Id),
	CONSTRAINT FK_DebateParticipant_ParticipantTypeID FOREIGN KEY(ParticipantTypeID) REFERENCES ParticipantType(Id)
)
