using UnityEngine;
using System.IO;
using System;
using System.Runtime.InteropServices;

namespace DogProject
{
    public class CameraManager : BaseManager
    {
        //[DllImport("__Internal")]
        //private static extern void _iosOpenPhotoLibrary();
        //[DllImport("__Internal")]
        //private static extern void _iosOpenPhotoAlbums();
        //[DllImport("__Internal")]
        //private static extern void _iosOpenCamera();
        //[DllImport("__Internal")]
        //private static extern void _iosOpenPhotoLibrary_allowsEditing();
        //[DllImport("__Internal")]
        //private static extern void _iosOpenPhotoAlbums_allowsEditing();
        //[DllImport("__Internal")]
        //private static extern void _iosOpenCamera_allowsEditing();
        [DllImport("__Internal")]
        private static extern void _iosSaveImageToPhotosAlbum(string readAddr);

        private RenderTexture render;
        private Texture2D texture;
        private string destination;
        private string fileName;
        private string savePath;

        private AndroidJavaClass jc;
        private AndroidJavaObject jo;

        public CameraManager(Facade facade) : base(facade)
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    destination = Application.streamingAssetsPath + "/Screenshots";
                    break;
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    destination = Application.persistentDataPath + "/Screenshots";
                    break;
            }
        }

        public override void OnInit()
        {
            InitAndroidClass();

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
        }

        private void InitAndroidClass()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                //获取当前activity对象(代表Android中的MainActivity)
                jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            }
        }

        public void ScreenShot()
        {
            render = new RenderTexture(Screen.width, Screen.height, 1);
            Camera.main.targetTexture = render;
            Camera.main.Render();
            RenderTexture.active = render;

            texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            byte[] bytes = texture.EncodeToJPG();

            Camera.main.targetTexture = null;
            RenderTexture.active = null;
            GameObject.Destroy(render);

            //string time = DateTime.Now.ToString().Trim().Replace("/", "-");
            //fileName = "ARScreenShot" + time + ".jpg";

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    fileName = "ARScreenshot_" + DateTime.Now.ToString("yyyMMddHHmmss") + ".jpg";
                    break;
                case RuntimePlatform.IPhonePlayer:
                case RuntimePlatform.Android:
                    fileName = "ARScreenshot.jpg";
                    break;
            }

            savePath = destination + "/" + fileName;

            File.WriteAllBytes(savePath, bytes);

            //保存到移动平台相册
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    iosSaveImageToPhotosAlbum(savePath);
                    break;
                case RuntimePlatform.Android:
                    androidSaveImageToPhotosAlbum(savePath);
                    break;
            }
        }

        /// <summary>
        /// Android保存图片到相册
        /// </summary>
        private void androidSaveImageToPhotosAlbum(string path)
        {
            jo.Call<string>("savePic", path);
        }

        /// <summary>
        /// ios保存图片到相册
        /// </summary>
        public static void iosSaveImageToPhotosAlbum(string readAddr)
        {
            _iosSaveImageToPhotosAlbum(readAddr);
        }
    }
}
