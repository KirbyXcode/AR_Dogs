using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DogProject
{
	public class UIStart : BasePanel 
	{
        private CanvasGroup cg;
        private Button mButton_Close;

        private void Awake()
        {
            cg = GetComponent<CanvasGroup>();
            cg.alpha = 0;
        }

        private void Start()
        {
            InitListener();
        }

        private void InitListener()
        {
            mButton_Close = Global.FindChild<Button>(transform, "Close");
            mButton_Close.onClick.AddListener(OnButtonClose);
        }

        private void OnButtonClose()
        {
            uiMgr.PopPanel();
        }

        private void PlayEnterAnimation()
        {
            if (!gameObject.activeInHierarchy)
                gameObject.SetActive(true);

            cg.DOFade(1, 0.4f);
        }

        private void PlayExitAnimation()
        {
            cg.DOFade(0, 0.4f).OnComplete(() => gameObject.SetActive(false));
        }

        public override void OnEnter()
        {
            PlayEnterAnimation();
			facade.PlaySound ();
        }

        public override void OnExit()
        {
            PlayExitAnimation();
        }

        public override void OnPause()
        {
            PlayExitAnimation();
        }

        public override void OnResume()
        {
            PlayEnterAnimation();
        }
    }
}
