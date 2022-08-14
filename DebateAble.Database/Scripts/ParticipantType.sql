MERGE INTO ParticipantType existing
USING (
	SELECT
		1 ID,
		'Debater' Name,
		1 CanDebate,
		1 CanComment,
		1 CanView

	UNION ALL
	SELECT
		2,
		'Commenter',
		0,
		1,
		1

	UNION ALL
	SELECT
		3,
		'Viewer',
		0,
		0,
		0
) incoming ON
	incoming.ID = existing.ID
WHEN NOT MATCHED THEN 
	INSERT (Id, Name, CanDebate,CanComment,CanView)
	VALUES(incoming.ID, incoming.NAme, incoming.CanDebate, incoming.CanComment, incoming.CanView)
;