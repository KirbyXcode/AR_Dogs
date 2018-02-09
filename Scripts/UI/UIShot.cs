using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DogProject
{
	public class UIShot : BasePanel 
	{
		private Button mButton_Close;
	
		void Awake()
		{
			transform.localScale = Vector3.zero;
		}
		
		private void Start()
        {
            InitListener();
        }
		
		private void InitListener()
		{
			mButton_Close = Global.FindChild<Button> (transform, "CloseButton");
			mButton_Close.onClick.AddListener (OnButtonClose);
		}
		
		private void OnButtonClose()
		{
			uiMgr.PopPanel();
		}
		
		private void EnterAnim()
		{
			if(!gameObject.activeInHierarchy)
				gameObject.SetActive (true);
            transform.DOScale(1, 0.4f);
		}

		private void ExitAnim()
		{
			transform.DOScale (0, 0.4f).OnComplete (() => gameObject.SetActive (false));
		}
		
		public override void OnEnter()
        {
            EnterAnim();
        }

        public override void OnPause()
        {
            ExitAnim();
        }

        public override void OnResume()
        {
            EnterAnim();
        }

        public override void OnExit()
        {
            ExitAnim();
        }
	}
}
