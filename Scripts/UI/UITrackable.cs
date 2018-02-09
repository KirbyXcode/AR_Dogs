using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EventCenter;

namespace DogProject
{
	public class UITrackable :  BasePanel
	{
        private CanvasGroup mCg_Window;
        private CanvasGroup mCg_Dialogue;
        private Button mButton_Close;
        private Text mText_MainContent;
        private Text mText_SubContent;
        private Image mImage_Dog;

        private Vector3 enterLocPos;
        private Vector3 exitLocPos;
        private Color transparency = new Color(1, 1, 1, 0);

        private void Awake()
        {
            TrackEvent.TrackableEvent += TrackableEvent;
            TrackEvent.LostEvent += LostEvent;
        }

        private void OnDestroy()
        {
            TrackEvent.TrackableEvent -= TrackableEvent;
            TrackEvent.LostEvent -= LostEvent;
        }

        private void TrackableEvent(string trackableName)
        {
            PlayEnterAnimation(trackableName);
        }

        private void LostEvent(string trackableName)
        {
            PlayExitAnimation();
        }

        private void Start()
        {
            mCg_Window = Global.FindChild<CanvasGroup>(transform, "Window");
            mCg_Dialogue = Global.FindChild<CanvasGroup>(transform, "Dialogue");

            mText_MainContent = Global.FindChild<Text>(transform, "MainContent");
            mText_SubContent = Global.FindChild<Text>(transform, "SubContent");

            mImage_Dog = Global.FindChild<Image>(transform, "Dog");

            exitLocPos = mCg_Dialogue.transform.localPosition;
            enterLocPos = mCg_Dialogue.transform.localPosition - new Vector3(0, 161, 0);

            InitListener();

            gameObject.SetActive(false);
        }

        private void InitListener()
        {
            mButton_Close = Global.FindChild<Button>(transform, "Close");
            mButton_Close.onClick.AddListener(OnButtonClose);
        }

        private void OnButtonClose()
        {
            PlayExitAnimation();
        }

        public void PlayEnterAnimation(string trackableName)
        {
            switch (trackableName)
            {
                case Define.TRACK_NAME_001:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_01;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_01;
                    break;
                case Define.TRACK_NAME_002:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_02;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_02;
                    break;
                case Define.TRACK_NAME_003:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_03;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_03;
                    break;
                case Define.TRACK_NAME_004:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_04;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_04;
                    break;
                case Define.TRACK_NAME_005:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_05;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_05;
                    break;
                case Define.TRACK_NAME_006:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_06;
                    mText_SubContent.text = Define.AR_DOG_06;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_06;
                    break;
                case Define.TRACK_NAME_007:
					ShowPanel();
                    mText_SubContent.text = Define.AR_DOG_07;
                    mText_MainContent.text = Define.AR_DOG_CONTENT_07;
                    break;
            }
        }
		
		private void ShowPanel()
		{
			if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);

                Transform windowTrans = mCg_Window.transform;

                if (windowTrans.localScale != Vector3.zero)
                    windowTrans.localScale = Vector3.zero;

                if (mCg_Window.alpha != 0)
                    mCg_Window.alpha = 0;

                mCg_Window.DOFade(1, 0.3f);
                windowTrans.DOScale(1, 0.4f);

                if (mImage_Dog.color.a != 0)
                    mImage_Dog.color = transparency;

                mImage_Dog.DOFade(1, 0.4f);

                if (mCg_Dialogue.alpha != 0)
                    mCg_Dialogue.alpha = 0;

                mCg_Dialogue.DOFade(1, 0.3f);

                Transform dialogueTrans = mCg_Dialogue.transform;

                if (dialogueTrans.localPosition != enterLocPos)
                    dialogueTrans.localPosition = enterLocPos;

                dialogueTrans.DOLocalMove(exitLocPos, 0.4f);

                //transform.SetAsLastSibling();

				facade.PlaySound ();
            }
		}

        public void PlayExitAnimation()
        {
            Transform windowTrans = mCg_Window.transform;
            mCg_Window.DOFade(0, 0.3f);
            windowTrans.DOScale(0,0.4f).OnComplete(() => gameObject.SetActive(false));

            mImage_Dog.DOFade(0, 0.4f);

            Transform dialogueTrans = mCg_Dialogue.transform;
            mCg_Dialogue.DOFade(0, 0.3f);
            dialogueTrans.DOLocalMove(enterLocPos, 0.4f);
        }
    }
}
