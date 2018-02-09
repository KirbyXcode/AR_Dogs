using UnityEngine;

namespace DogProject
{
	public class CharacterAnimation : MonoBehaviour 
	{
        private Animation anim;

        private void Start()
        {
            anim = GetComponent<Animation>();
            EventCenter.AnimationEvent.PlayAnimationEvent += PlayAnimationEvent;
        }

        private void PlayAnimationEvent(string animName)
        {
            if (!GetComponentInChildren<SkinnedMeshRenderer>().enabled) return;

            switch (animName)
            {
                case Define.AnimIdle:
                    PlayAnimation(Define.AnimIdle);
                    break;
                case Define.AnimWalk:
                    PlayAnimation(Define.AnimWalk);
                    break;
                case Define.AnimRun:
                    PlayAnimation(Define.AnimRun);
                    break;
                case Define.AnimAttack:
                    PlayAnimation(Define.AnimAttack);
                    break;
            }
        }

        public void PlayAnimation(string animName)
        {
            anim.CrossFade(animName);
        }

        private void OnDestroy()
        {
            EventCenter.AnimationEvent.PlayAnimationEvent -= PlayAnimationEvent;
        }
    }
}
