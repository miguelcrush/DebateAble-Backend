CREATE TABLE [dbo].[ResponseRequest]
(
	Id uniqueidentifier NOT NULL DEFAULT (newsequentialid()),
	DebateId uniqueidentifier NOT NULL,
	ParticipantId uniqueidentifier NOT NULL,
	Satisfied bit NOT NULL DEFAULT(0),
	ExpiresOnUtc datetime2 NOT NULL,
	CreatedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	ModifiedOnUtc datetime2 NOT NULL DEFAULT(getutcdate()),
	CONSTRAINT PK_ResponseRequest PRIMARY KEY(Id),
	CONSTRAINT FK_ResponseRequest_DebateId FOREIGN KEY(DebateId) REFERENCES Debate(Id),
	CONSTRAINT FK_ResponseRequest_ParticipantId FOREIGN KEY(ParticipantId) REFERENCES DebateParticipant(Id)
)
