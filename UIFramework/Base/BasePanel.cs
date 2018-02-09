using UnityEngine;

namespace DogProject
{

    public class BasePanel : MonoBehaviour
    {
        protected Facade facade;
        public Facade _facade
        {
            set { facade = value; }
        }
        protected UIManager uiMgr;
        public UIManager _uiMgr
        {
            set { uiMgr = value; }
        }

        /// <summary>
        /// 界面显示
        /// </summary>
        public virtual void OnEnter()
        {

        }

        /// <summary>
        /// 次级界面隐藏
        /// </summary>
        public virtual void OnPause()
        {

        }

        /// <summary>
        /// 次级界面显示
        /// </summary>
        public virtual void OnResume()
        {

        }

        /// <summary>
        /// 界面隐藏
        /// </summary>
        public virtual void OnExit()
        {

        }
    }
}
