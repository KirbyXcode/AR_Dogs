using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DogProject
{

	public class UIAbout : BasePanel 
	{
		private Button mButton_Close;
		private Button mButton_OK;

		void Awake()
		{
			transform.localScale = Vector3.zero;
		}

		void Start()
		{
			mButton_Close = Global.FindChild<Button> (transform, "CloseButton");
			mButton_OK = Global.FindChild<Button> (transform, "OKButton");

			mButton_Close.onClick.AddListener (OnButtonClose);
			mButton_OK.onClick.AddListener (OnButtonClose);
		}

		private void OnButtonClose()
		{
			uiMgr.PopPanel ();
		}

		private void EnterAnim()
		{
			if(!gameObject.activeInHierarchy)
				gameObject.SetActive (true);
			transform.DOScale (1, 0.4f);
		}

		private void ExitAnim()
		{
			transform.DOScale (0, 0.4f).OnComplete (() => gameObject.SetActive (false));
		}

		public override void OnEnter ()
		{
			EnterAnim ();
		}

		public override void OnExit ()
		{
			ExitAnim ();
		}

		public override void OnResume ()
		{
			EnterAnim ();
		}

		public override void OnPause ()
		{
			ExitAnim ();
		}

	}
}