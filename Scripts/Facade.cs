using DevelopEngine;

namespace DogProject
{
	public class Facade : MonoSingleton<Facade> 
	{
        private UIManager uiMgr;
        private VuforiaManager vuforiaMgr;
		private AudioManager audioMgr;
		private CameraManager cameraMgr;

        private void Start()
        {
            InitManagers();
        }

        //private void Update()
        //{
        //    UpdateManagers();
        //}

        private void OnDestroy()
        {
            DestroyManagers();
        }

        private void InitManagers()
        {
            uiMgr = new UIManager(this);
            vuforiaMgr = new VuforiaManager(this);
			audioMgr = new AudioManager(this);
			cameraMgr = new CameraManager(this);

            uiMgr.OnInit();
            vuforiaMgr.OnInit();
			audioMgr.OnInit();
			cameraMgr.OnInit();
        }

        private void UpdateManagers()
        {
            uiMgr.OnUpdate();
            vuforiaMgr.OnUpdate();
        }

        private void DestroyManagers()
        {
            uiMgr.OnDestroy();
            vuforiaMgr.OnDestroy();
			audioMgr.OnDestroy ();
        }

        public void PushPanel(UIPanelType panelType)
        {
            uiMgr.PushPanel(panelType);
        }

        public void PopPanel()
        {
            uiMgr.PopPanel();
        }

		public void PlaySound(float volume = 0.5f, bool isLoop = false)
		{
			audioMgr.PlaySound (volume, isLoop);
		}

        public void ScreenShot()
        {
            cameraMgr.ScreenShot();
        }
    }
}
