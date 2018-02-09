namespace EventCenter
{
	public class TrackEvent 
	{
        public delegate void TrackableHandler(string trackableName);
        public static event TrackableHandler TrackableEvent;
        public static void OnTrackableEvent(string trackableName)
        {
            if (TrackableEvent != null)
                TrackableEvent(trackableName);
        }

        public delegate void LostHandler(string trackableName);
        public static event LostHandler LostEvent;
        public static void OnLostEvent(string trackableName)
        {
            if (LostEvent != null)
                LostEvent(trackableName);
        }
	}
}
