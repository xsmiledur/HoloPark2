using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.XR.WSA.WebCam;
using System.Linq;

namespace HoloToolkit.Unity {
    public class GameBehavior : MonoBehaviour
    {
        [DllImport("CppPlugin")] private static extern int Add(int a, int b);
        [DllImport("CppPlugin")] private static extern int Sum(byte[] data, int height, int width);

        private PhotoCapture capture = null;
        private Resolution resolution;
        private CameraParameters c;
        private int width, height;
        private Texture2D tex;

        public GameObject obj; // 画像処理判定が正のとき登場させるオブジェクト Inspectorから指定


        // Use this for initialization
        void Start()
        {
            return;
            // test
            Debug.Log(Sum(new byte[] { 1, 2, 3, 4, 5, 6 }, 3, 2));

            PhotoCapture.CreateAsync(false, (_capture) =>
            {
                capture = _capture;
                resolution = PhotoCapture.SupportedResolutions.OrderByDescending(res => res.width * res.height).First();
                width = resolution.width;
                height = resolution.height;

                tex = new Texture2D(width, height);
                c = new CameraParameters(WebCamMode.PhotoMode);
                c.hologramOpacity = 1.0f;
                c.cameraResolutionWidth = width;
                c.cameraResolutionHeight = height;
                c.pixelFormat = CapturePixelFormat.BGRA32; capture.StartPhotoModeAsync(c, OnPhotoModeStarted);
                Debug.Log("StartPhotoModeAsync");
            });

        }


        int frame = 0;
        // Update is called once per frame
        void Update()
        {
            return;
            //Debug.Log(frame);
            if (frame > 100)
            {
                frame = 0;
                if (startFlg)
                {
                    capture.TakePhotoAsync(OnCapturedPhotoToMemory);

                }


            }
            ++frame;
        }
        bool startFlg = false;
        private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
        {
            if (result.success)
            {
                startFlg = true;
                Debug.Log("PhotoMode started");
            }
            else
            {
                //Debug.LogError("Unable to start photo mode!");
                Debug.Log("Unable to start photo mode!");
            }
        }


        private void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
        {
            if (result.success)
            {
                Resolution cameraResolution = PhotoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
                photoCaptureFrame.UploadImageDataToTexture(tex);

                // textureをbyte配列に変換 png,JPGどちらでも．pngのほうがdata.Lengthが大きいみたい
                byte[] data = tex.EncodeToPNG();
                //byte[] data = tex.EncodeToJPG();

                // ここでdata.Lengthが変動するため，Sum(data, height, width)が失敗する→アプリが落ちる
                Debug.Log(data.Length + " width: " + cameraResolution.width + " height: " + cameraResolution.height);
                //Debug.Log(num + " " + Sum(data, height, width));

                // ここに画像処理CppLibraryにつなげるコードを記述．

                // 処理結果正ならオブジェクトを出す
                // 第２，３引数に位置と回転を入れられたはず
                //Instantiate(obj);
            }
            // Clean up
            // 本当はReleaseした方がいいけどタイミングが無い
            //capture.StopPhotoModeAsync(OnStoppedPhotoMode);
        }

        void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
        {
            capture.Dispose();
            capture = null;
        }

    }

}