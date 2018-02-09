using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace DogProject
{
    public class UICamera : BasePanel
    {
        private Button mButton_Exit;

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }

        private void Start()
        {
            mButton_Exit = Global.FindChild<Button>(transform, "ExitButton");
            mButton_Exit.onClick.AddListener(OnButtonExit);
        }

        private void OnButtonExit()
        {
            Application.Quit();
        }

        public override void OnEnter()
        {
            transform.DOScale(1, 0.4f);
        }
    }
}
