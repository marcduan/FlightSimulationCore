using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;  // For UI display (optional)

public class FlightController : MonoBehaviour
{
    // API URL (change this if you host Flask on a different server)
    private string apiURL = "http://127.0.0.1:5000/predict"; // Localhost for development

    // UI elements to display the result (optional)
    public Text turbulenceResultText;

    // Method to send weather data and get turbulence prediction
    public void GetTurbulencePrediction(float temperature, float altitude, float windSpeed)
    {
        StartCoroutine(SendPredictionRequest(temperature, altitude, windSpeed));
    }

    // Coroutine to send weather data to Flask API and get the result
    private IEnumerator SendPredictionRequest(float temperature, float altitude, float windSpeed)
    {
        // Prepare the JSON data to send in the request
        string jsonData = "{\"temperature\": " + temperature + ", \"altitude\": " + altitude + ", \"wind_speed\": " + windSpeed + "}";

        // Create the UnityWebRequest to send the POST request
        UnityWebRequest www = UnityWebRequest.Put(apiURL, jsonData);
        www.method = UnityWebRequest.kHttpVerbPOST;  // Set the HTTP method to POST
        www.SetRequestHeader("Content-Type", "application/json");  // Specify that the body is JSON

        // Wait for the request to complete
        yield return www.SendWebRequest();

        // Check for errors
        if (www.result == UnityWebRequest.Result.Success)
        {
            // Parse the response JSON and extract turbulence prediction
            string response = www.downloadHandler.text;
            Debug.Log("Response: " + response);

            // (Optional) Update the UI with the result
            if (turbulenceResultText != null)
            {
                turbulenceResultText.text = "Predicted Turbulence: " + response;
            }
        }
        else
        {
            // Handle error if the request fails
            Debug.LogError("Error: " + www.error);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Trigger prediction when Space is pressed
        {
            // Sample weather data (you can dynamically change these values based on your simulation)
            float temperature = 30f;  // Temperature in Celsius
            float altitude = 1200f;    // Altitude in meters
            float windSpeed = 15f;     // Wind speed in km/h

            // Call the method to send data and get the turbulence prediction
            GetTurbulencePrediction(temperature, altitude, windSpeed);
        }
    }

}
