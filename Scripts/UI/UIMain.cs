using UnityEngine;
using UnityEngine.UI;

namespace DogProject
{
	public class UIMain : BasePanel 
	{
        private Button mButton_Quit;
		private Button mButton_About;
		private Button mButton_Screenshot;

        private void Start()
        {
            InitListener();
        }

        private void InitListener()
        {
            mButton_Quit = Global.FindChild<Button>(transform, "Quit");
            mButton_Quit.onClick.AddListener(OnButtonQuit);

			mButton_About = Global.FindChild<Button> (transform, "About");
			mButton_About.onClick.AddListener (OnButtonAbout);
			
			mButton_Screenshot = Global.FindChild<Button>(transform, "ScreenShot");
			mButton_Screenshot.onClick.AddListener( OnButtonScreenshot);
        }

		private void OnButtonAbout()
		{
			uiMgr.PushPanel (UIPanelType.About);
		}

		private void OnButtonQuit()
		{
			Application.Quit ();
		}
		
		private void OnButtonScreenshot()    
		{
            facade.ScreenShot();
			uiMgr.PushPanel(UIPanelType.Shot);
		}

        public override void OnEnter()
        {
            gameObject.SetActive(true);
        }
    }
}
