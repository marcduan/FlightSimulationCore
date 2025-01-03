/*
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FlightDataLogger : MonoBehaviour
{
    private string apiUrl = "http://127.0.0.1:5000/log-flight"; // URL for your Flask API endpoint

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press space to simulate sending flight data
        {
            // Example flight data
            string time = "12:05:00";
            float speed = 300f;
            float altitude = 15000f;
            float fuelLevel = 80f;
            string weatherCondition = "clear";

            // Send data to Flask API
            StartCoroutine(SendFlightData(time, speed, altitude, fuelLevel, weatherCondition));
        }
    }

    // Coroutine to send data to Flask API
    IEnumerator SendFlightData(string time, float speed, float altitude, float fuelLevel, string weatherCondition)
    {
        // Create JSON data
        string jsonData = "{\"time\": \"" + time + "\", \"speed\": " + speed + ", \"altitude\": " + altitude +
                          ", \"fuel_level\": " + fuelLevel + ", \"weather_condition\": \"" + weatherCondition + "\"}";

        // Create a UnityWebRequest to POST data
        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // Wait for the response
            yield return request.SendWebRequest();

            // Handle response
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data sent successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error sending data: " + request.error);
            }
        }
    }
}
*/

/*
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class FlightDataLogger : MonoBehaviour
{
    public AirplaneController airplaneController; // Reference to your airplane simulation script
    private string apiUrl = "http://127.0.0.1:5000/log-flight";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to log data
        {
            // Get data from the simulation
            string time = DateTime.Now.ToString("HH:mm:ss"); // Current time
            float speed = airplaneController.GetCurrentSpeed(); // Airplane's current speed
            float altitude = airplaneController.GetCurrentAltitude(); // Airplane's current altitude
            float fuelLevel = airplaneController.GetCurrentFuelLevel(); // Airplane's current fuel level
            //string weatherCondition = WeatherSystem.GetCurrentWeatherCondition(); // Current weather condition (optional)

            // Send data to the Flask API
            StartCoroutine(SendFlightData(time, speed, altitude, fuelLevel));
        }
    }

    // Send data to Flask API
    IEnumerator SendFlightData(string time, float speed, float altitude, float fuelLevel)
    {
        string jsonData = "{\"time\": \"" + time + "\", \"speed\": " + speed + ", \"altitude\": " + altitude +
                          ", \"fuel_level\": " + fuelLevel + "}";

        using (UnityWebRequest request = new UnityWebRequest(apiUrl, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Data sent successfully: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error sending data: " + request.error);
            }
        }
    }
}
*/

/*
using System.IO;
using System;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine;

public class FlightDataLogger : MonoBehaviour
{
    private AirplaneController airplane;
    //private WeatherSystem weather;

    void Start()
    {
        // Reference the AirplaneController attached to the same GameObject
        airplane = GetComponent<AirplaneController>();

        // Reference the WeatherSystem (attach WeatherSystem to a GameObject in your scene)
        //weather = FindObjectOfType<WeatherSystem>();

        if (airplane == null)
            Debug.LogError("AirplaneController not found!");
        //if (weather == null)
            //Debug.LogError("WeatherSystem not found!");
    }

    void LogFlightData()
    {
        //if (airplane == null || weather == null) return;

        // Get data from AirplaneController
        float speed = airplane.GetCurrentSpeed();
        float altitude = airplane.GetCurrentAltitude();
        float fuelLevel = airplane.GetCurrentFuelLevel();

        // Get weather condition
        //string weatherCondition = weather.GetCurrentWeatherCondition();

        // Log data (replace this with your desired logging method)
        Debug.Log($"Speed: {speed}, Altitude: {altitude}, Fuel: {fuelLevel}"); //Debug.Log($"Speed: {speed}, Altitude: {altitude}, Fuel: {fuelLevel}, Weather: {weatherCondition}");
    }
}
*/


/*
using System.IO;
using UnityEngine;

public class FlightDataLogger : MonoBehaviour
{
    private AirplaneController airplane;

    private string filePath;

    void Start()
    {
        airplane = GetComponent<AirplaneController>();
        if (airplane == null)
        {
            Debug.LogError("AirplaneController not found on this GameObject!");
            airplane = gameObject.AddComponent<AirplaneController>();
        }
        else
        {
            Debug.Log("AirplaneController found and attached to this GameObject.");
        }
        

        // Define file path
        filePath = Path.Combine(Application.dataPath, "flight_data.csv");

        // Create file with headers if it doesn't exist
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "time,speed,altitude,fuel_level\n");
        }
    }

    void Update()
    {
        // Log data only when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            LogFlightData(); 
        }
    }

    private void LogFlightData()
    {
        if (airplane == null) return;

        // Get flight data
        float speed = airplane.GetCurrentSpeed();
        float altitude = airplane.GetCurrentAltitude();
        float fuelLevel = airplane.GetCurrentFuelLevel();

        // Format data as CSV
        string dataLine = $"{Time.time},{speed},{altitude},{fuelLevel}\n";

        // Append data to the file
        File.AppendAllText(filePath, dataLine);

        Debug.Log($"Logged Data: {dataLine}");
    }
}
*/


using System.IO;
using UnityEngine;

public class FlightDataLogger : MonoBehaviour
{
    private AirplaneController airplane;
    private string filePath;

    void Start()
    {
        airplane = GetComponent<AirplaneController>();
        if (airplane == null)
        {
            Debug.LogError("AirplaneController not found on this GameObject!");
            airplane = gameObject.AddComponent<AirplaneController>();
        }
        else
        {
            Debug.Log("AirplaneController found and attached to this GameObject.");
        }

        // Define file path
        filePath = Path.Combine(Application.dataPath, "flight_data.csv");

        // Create file with headers if it doesn't exist
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "time,speed,altitude,fuel_level,weather_condition\n");
        }
    }

    void Update()
    {
        // Log data only when the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogFlightData();
        }
    }

    private void LogFlightData()
    {
        if (airplane == null) return;

        // Get flight data
        float speed = airplane.GetCurrentSpeed();
        float altitude = airplane.GetCurrentAltitude();
        float fuelLevel = airplane.GetCurrentFuelLevel();

        // Determine weather condition based on flight data
        string weatherCondition = GetWeatherCondition(speed, altitude, fuelLevel);

        // Format data as CSV
        string dataLine = $"{Time.time},{speed},{altitude},{fuelLevel},{weatherCondition}\n";

        // Append data to the file
        File.AppendAllText(filePath, dataLine);

        Debug.Log($"Logged Data: {dataLine}");
    }

    private string GetWeatherCondition(float speed, float altitude, float fuelLevel)
    {
        // Example logic for weather conditions
        if (altitude > 300)
            return "turbulent"; // High altitude -> turbulence
        else if (fuelLevel < 80)
            return "foggy"; // Low fuel -> possible fog near landing
        else if (speed > 4.5 && altitude < 300)
            return "clear"; // High speed and low altitude -> clear skies
        else
            return "cloudy"; // Default condition
    }
}
