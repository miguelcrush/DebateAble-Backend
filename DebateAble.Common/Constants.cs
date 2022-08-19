namespace DebateAble.Common
{
	public enum ParticipantTypeEnum
	{
		Unknown = 0,
		Debater = 1,
		Commenter = 2,
		Viewer = 3 
	}

	[Flags]
	public enum DebateIncludes
    {
		None = 0,
		Posts = 1 << 0,
		Comments = 1 <<1,
		Participants = 1<<2,
		ResponseRequests = 1<<3
    }
}