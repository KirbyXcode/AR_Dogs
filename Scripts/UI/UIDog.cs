using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using Vuforia;
using EventCenter;

namespace DogProject
{
	public class UIDog : BasePanel 
	{
		private Button mButton_Idle;
		private Button mButton_Walk;
		private Button mButton_Run;
		private Button mButton_Attack;
		private Button mButton_Virtual;
		private Text mText_Virtual;
		private Button mButton_Enable;
		private Button mButton_Disable;

		private CanvasGroup cg;
		private CanvasGroup mCG_VirtualSwitch;
		private int switchOffset = 150;
		private Transform imageTargetsTrans;

		private List<VirtualButtonBehaviour> vbList;

		private Vector3 enterLocPos;
		private Vector3 exitLocPos;

		private Vector3 originalPos;
		private Vector3 targetPos;

		private void Awake()
		{
			cg = GetComponent<CanvasGroup>();

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
			switch (trackableName) {
			case Define.TRACK_DOG_NAME_01:
			case Define.TRACK_DOG_NAME_02:
			case Define.TRACK_DOG_NAME_03:
			case Define.TRACK_DOG_NAME_04:
			case Define.TRACK_DOG_NAME_05:
			case Define.TRACK_DOG_NAME_06:
			case Define.TRACK_DOG_NAME_07:
			case Define.TRACK_DOG_NAME_08:
			case Define.TRACK_DOG_NAME_09:
				SetPanelActive (true);
				break;
			}
		}

		private void LostEvent(string trackableName)
		{
			switch (trackableName) 
			{
				case Define.TRACK_DOG_NAME_01:
				case Define.TRACK_DOG_NAME_02:
				case Define.TRACK_DOG_NAME_03:
				case Define.TRACK_DOG_NAME_04:
				case Define.TRACK_DOG_NAME_05:
				case Define.TRACK_DOG_NAME_06:
				case Define.TRACK_DOG_NAME_07:
				case Define.TRACK_DOG_NAME_08:
				case Define.TRACK_DOG_NAME_09:
					SetPanelActive (false);
					break;
			}
		}

		private void Start()
		{
			mCG_VirtualSwitch = Global.FindChild<CanvasGroup>(transform, "VirtualSwitch");
			originalPos = mCG_VirtualSwitch.transform.localPosition;
			targetPos = originalPos + new Vector3(193, 0, 0);

			InitVirtualSwitch();

			InitVirtualButtons();

			InitListener();

			exitLocPos = transform.localPosition;
			enterLocPos = exitLocPos + new Vector3(250, 0, 0);

			gameObject.SetActive (false);
		}

		private void InitListener()
		{
			mButton_Idle = Global.FindChild<Button>(transform, "Idle");
			mButton_Walk = Global.FindChild<Button>(transform, "Walk");
			mButton_Run = Global.FindChild<Button>(transform, "Run");
			mButton_Attack = Global.FindChild<Button>(transform, "Attack");
			mButton_Virtual = Global.FindChild<Button>(transform, "VirtualButton");
			mText_Virtual = Global.FindChild<Text>(transform, "VirtualText");
			mButton_Enable = Global.FindChild<Button>(transform, "Enable");
			mButton_Disable = Global.FindChild<Button>(transform, "Disable");

			mButton_Idle.onClick.AddListener(OnButtonIdle);
			mButton_Walk.onClick.AddListener(OnButtonWalk);
			mButton_Run.onClick.AddListener(OnButtonRun);
			mButton_Attack.onClick.AddListener(OnButtonAttack);
			mButton_Virtual.onClick.AddListener(OnButtonVirtual);
			mButton_Enable.onClick.AddListener(OnButtonVirtualEnable);
			mButton_Disable.onClick.AddListener(OnButtonVirtualDisable);
		}

		private void InitVirtualButtons()
		{
			vbList = new List<VirtualButtonBehaviour>();
			imageTargetsTrans = GameObject.Find("ImageTargetManager").transform;

			vbList = imageTargetsTrans.GetComponentsInChildren<VirtualButtonBehaviour>().ToList();

			DisableVirtualButtons();
		}

		private void InitVirtualSwitch()
		{
			Transform trans = mCG_VirtualSwitch.transform;
			trans.localPosition = targetPos;
			mCG_VirtualSwitch.alpha = 0;
			trans.gameObject.SetActive(false);
		}

		private void DisableVirtualButtons()
		{
			foreach (var vb in vbList)
			{
				vb.enabled = false;
				vb.gameObject.SetActive(false);
			}
		}

		private void EnableVirtualButtons()
		{
			foreach (var vb in vbList)
			{
				vb.gameObject.SetActive(true);
				vb.enabled = true;
			}
		}

		private void OnButtonVirtualDisable()
		{
			DisableVirtualButtons();
			PlayExitVirtualAnimation();
			mText_Virtual.text = "虚拟按钮(关闭)";
		}

		private void OnButtonVirtualEnable()
		{
			EnableVirtualButtons();
			PlayExitVirtualAnimation();
			mText_Virtual.text = "虚拟按钮(开启)";
		}

		private void OnButtonIdle()
		{
			EventCenter.AnimationEvent.OnPlayAnimation(Define.AnimIdle);
		}

		private void OnButtonWalk()
		{
			EventCenter.AnimationEvent.OnPlayAnimation(Define.AnimWalk);
		}

		private void OnButtonRun()
		{
			EventCenter.AnimationEvent.OnPlayAnimation(Define.AnimRun);
		}

		private void OnButtonAttack()
		{
			EventCenter.AnimationEvent.OnPlayAnimation(Define.AnimAttack);
		}

		private void OnButtonVirtual()
		{
			PlayEnterVirtualAnimation();
		}

		private void OnButtonQuit()
		{
			uiMgr.PushPanel(UIPanelType.Quit);
		}

		private void PlayEnterVirtualAnimation()
		{
			Transform trans = mCG_VirtualSwitch.transform;
			trans.gameObject.SetActive(true);
			trans.DOLocalMove(originalPos, 0.4f);
			mCG_VirtualSwitch.DOFade(1, 0.3f);
		}

		private void PlayExitVirtualAnimation()
		{
			Transform trans = mCG_VirtualSwitch.transform;
			trans.DOLocalMove(targetPos, 0.4f).OnComplete(() => trans.gameObject.SetActive(false));
			mCG_VirtualSwitch.DOFade(0, 0.3f);
		}

		public void SetPanelActive(bool isShow)
		{
			if (isShow)
			{
				gameObject.SetActive(true);
				cg.DOFade(1, 0.3f);
				transform.DOLocalMove(exitLocPos, 0.4f);
			}
			else
			{
				cg.DOFade(0, 0.3f);
				transform.DOLocalMove(enterLocPos, 0.4f).OnComplete(() => gameObject.SetActive(false));
			}
		}
	}
}
