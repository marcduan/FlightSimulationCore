
/*
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    public GameObject airplane; // Reference to your airplane object
    private StreamWriter writer;

    void Start()
    {
        // Create a new CSV file to log the data
        writer = new StreamWriter("flight_data.csv", true);
        writer.WriteLine("Time,Speed,Altitude,FuelLevel,WeatherCondition");
    }

    void Update()
    {
        // Log data every frame
        float speed = airplane.GetComponent<Rigidbody>().velocity.magnitude;  // Get speed
        float altitude = airplane.transform.position.y;  // Get altitude
        float fuelLevel = Mathf.Max(0, 100 - altitude / 10);  // Example: Decaying fuel as altitude increases
        string weatherCondition = "Clear";  // Placeholder: Replace with actual weather data (can be added later)

        // Write the data to the CSV file
        writer.WriteLine($"{Time.time},{speed},{altitude},{fuelLevel},{weatherCondition}");
    }

    void OnApplicationQuit()
    {
        // Close the writer when the application ends
        writer.Close();
    }
}
*/






/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class DataLogger : MonoBehaviour
{
    string filePath;

    void Start()
    {
        // Initialize file path for local testing
        filePath = Application.dataPath + "/Data/flight_data.csv";

        // Ensure the CSV file has a header row
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "time,speed,altitude,fuellevel,weathercondition\n");
            Debug.Log("CSV file initialized with header.");
        }
    }

    void Update()
    {
        // Example: Send data when the Space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string time = System.DateTime.Now.ToString("HH:mm:ss");
            float speed = Random.Range(200f, 500f); // Example: Random speed
            float altitude = Random.Range(1000f, 20000f); // Example: Random altitude
            float fuelLevel = Random.Range(0f, 100f); // Example: Random fuel level
            string weatherCondition = "clear"; // Example: Static weather

            // Send data to Flask server
            StartCoroutine(SendFlightData(time, speed, altitude, fuelLevel, weatherCondition));
        }
    }

    IEnumerator SendFlightData(string time, float speed, float altitude, float fuelLevel, string weatherCondition)
    {
        // Create data dictionary
        Dictionary<string, string> flightData = new Dictionary<string, string>
        {
            { "time", time },
            { "speed", speed.ToString() },
            { "altitude", altitude.ToString() },
            { "fuellevel", fuelLevel.ToString() },
            { "weathercondition", weatherCondition }
        };

        // Convert to JSON
        string jsonData = JsonUtility.ToJson(flightData);

        // Send data to Flask API
        UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:5000/log_data", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
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
*/

/*
using System.Collections;
using System.Collections.Generic;
//using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine;

public class DataLogger : MonoBehaviour
{
    private string filePath;

    void Start()
    {
        // Define the file path
        filePath = Application.dataPath + "/Data/flight_data.csv";

        // Check if the file exists; if not, write the header
        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data"); // Ensure the directory exists
            File.WriteAllText(filePath, "time,speed,altitude,fuellevel,weathercondition\n");
        }
    }

    public void LogFlightData(string time, float speed, float altitude, float fuelLevel, string weatherCondition)
    {
        // Format the data as a CSV row
        string dataLine = $"{time},{speed},{altitude},{fuelLevel},{weatherCondition}\n";

        // Append the data to the file
        File.AppendAllText(filePath, dataLine);

        // Optional: Debug log for verification
        Debug.Log($"Data logged: {dataLine}");
    }

    void Update()
    {
        // Example: Log data every time the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogFlightData("12:00", 300f, 15000f, 80f, "clear");
        }
    }
}
*/

/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    private string filePath;
    private AirplaneController airplane;

    void Start()
    {
        filePath = Application.dataPath + "/Data/flight_data.csv";

        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data"); // Ensure the directory exists
            File.WriteAllText(filePath, "time,speed,altitude,fuellevel,weathercondition\n");
        }

        airplane = GetComponent<AirplaneController>();
        if (airplane == null)
        {
            Debug.LogError("AirplaneController not found on this GameObject!");
        }
    }

    public void LogFlightData()
    {
        if (airplane == null) return;

        string time = System.DateTime.Now.ToString("HH:mm:ss");
        float speed = airplane.GetCurrentSpeed();
        float altitude = airplane.GetCurrentAltitude();
        float fuelLevel = airplane.GetCurrentFuelLevel();
        string weatherCondition = "clear"; // You can change this dynamically if you have weather data

        string dataLine = $"{time},{speed},{altitude},{fuelLevel},{weatherCondition}\n";
        File.AppendAllText(filePath, dataLine);

        Debug.Log($"Data logged: {dataLine}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogFlightData();
        }
    }
}
*/



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    private string filePath;
    private AirplaneController airplane;

    void Start()
    {
        filePath = Application.dataPath + "/Data/flight_data.csv";

        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data"); // Ensure the directory exists
            //File.WriteAllText(filePath, "time (HH:mm:ss),speed (m/s),altitude (m),fuel level (%),weather condition\n");
            File.WriteAllText(filePath, "time (HH:mm:ss), speed, altitude, fuel level, weather condition\n");
        }

        airplane = GetComponent<AirplaneController>();
        if (airplane == null)
        {
            Debug.LogError("AirplaneController not found on this GameObject!");
        }
    }

    public void LogFlightData()
    {
        if (airplane == null) return;

        string time = System.DateTime.Now.ToString("HH:mm:ss");
        float speed = airplane.GetCurrentSpeed();
        float altitude = airplane.GetCurrentAltitude();
        float fuelLevel = airplane.GetCurrentFuelLevel();
        string weatherCondition = "clear"; // You can change this dynamically if you have weather data

        string dataLine = $"{time} ,{speed}, {altitude}, {fuelLevel}, {weatherCondition}\n";
        File.AppendAllText(filePath, dataLine);

        Debug.Log($"Data logged: {dataLine}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogFlightData();
        }
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataLogger : MonoBehaviour
{
    private string filePath;
    private AirplaneController airplane;

    void Start()
    {
        filePath = Application.dataPath + "/Data/flight_data.csv";

        if (!File.Exists(filePath))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data"); // Ensure the directory exists
            File.WriteAllText(filePath, "time (HH:mm:ss), speed, altitude, fuel level, weather condition\n");
        }

        airplane = GetComponent<AirplaneController>();
        if (airplane == null)
        {
            Debug.LogError("AirplaneController not found on this GameObject!");
        }
    }

    public void LogFlightData()
    {
        if (airplane == null) return;

        string time = System.DateTime.Now.ToString("HH:mm:ss");
        float speed = airplane.GetCurrentSpeed();
        float altitude = airplane.GetCurrentAltitude();
        float fuelLevel = airplane.GetCurrentFuelLevel();

        // Determine weather condition dynamically based on the flight data
        string weatherCondition = GetWeatherCondition(speed, altitude, fuelLevel);

        string dataLine = $"{time}, {speed}, {altitude}, {fuelLevel}, {weatherCondition}\n";
        File.AppendAllText(filePath, dataLine);

        Debug.Log($"Data logged: {dataLine}");
    }

    private string GetWeatherCondition(float speed, float altitude, float fuelLevel)
    {
        // Example weather condition logic
        if (altitude > 1000)
            return "turbulent"; // High altitude -> turbulence
        else if (fuelLevel < 80)
            return "foggy"; // Low fuel -> foggy
        else if (speed > 4.5 && altitude < 1000)
            return "clear"; // High speed and low altitude -> clear skies
        else
            return "cloudy"; // Default condition
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LogFlightData();
        }
    }
}



