using System.IO;
using UnityEngine;

public class LandingPadFeedback : MonoBehaviour
{
    public string filePath = "detection_status.txt";

    void Update()
    {
        if (File.Exists(filePath))
        {
            string status = File.ReadAllText(filePath);
            Debug.Log(status);
        }
    }
}
