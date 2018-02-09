namespace EventCenter
{
	public class AnimationEvent 
	{
        public delegate void PlayAnimationHandler(string animName);
        public static event PlayAnimationHandler PlayAnimationEvent;
        public static void OnPlayAnimation(string animName)
        {
            if (PlayAnimationEvent != null)
            {
                PlayAnimationEvent(animName);
            }
        }
	}
}
