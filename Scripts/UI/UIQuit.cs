using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DogProject
{
	public class UIQuit : BasePanel 
	{
        private Button mButton_Close;
        private Button mButton_Apply;
        private CanvasGroup cg;

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
            mButton_Close = Global.FindChild<Button>(transform, "DeclineButton");
            mButton_Close.onClick.AddListener(OnButtonClose);

            mButton_Apply = Global.FindChild<Button>(transform, "ApplyButton");
            mButton_Apply.onClick.AddListener(OnButtonApply);
        }

        private void OnButtonApply()
        {
            Application.Quit();
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
        }

        public override void OnPause()
        {
            PlayExitAnimation();
        }

        public override void OnResume()
        {
            PlayEnterAnimation();
        }

        public override void OnExit()
        {
            PlayExitAnimation();
        }
    }
}
