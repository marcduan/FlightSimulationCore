using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotCapture : MonoBehaviour
{
    public string folderPath = "Screenshots/";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Press 'P' to capture
        {
            string path = folderPath + "screenshot_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";
            ScreenCapture.CaptureScreenshot(path);
            Debug.Log("Screenshot saved to: " + path);
        }
    }
}
