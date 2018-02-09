using Vuforia;

namespace DogProject
{
	public class VuforiaManager : BaseManager
	{
        private VuforiaARController controller;

        public VuforiaManager(Facade facade) : base(facade)
        {
            controller = VuforiaARController.Instance;
            controller.RegisterVuforiaStartedCallback(OnVuforiaStart);
        }

        public override void OnDestroy()
        {
            controller.UnregisterVuforiaStartedCallback(OnVuforiaStart);
        }

        private void OnVuforiaStart()
        {
            Global.IsCameraEnable = IsCameraEnable();

            if (!Global.IsCameraEnable) return;

            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
            facade.PushPanel(UIPanelType.Start);
        }

        private bool IsCameraEnable()
        {
            if(!VuforiaRuntimeUtilities.IsWebCamUsed())
            {
                facade.PushPanel(UIPanelType.Camera);
                return false;
            }
            return true;
        }
    }
}
