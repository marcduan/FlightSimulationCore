using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class FlightUIController : MonoBehaviour
{
    public Slider thrustSlider;
    public TMP_Text thrustText;
    public Slider weightSlider;
    public TMP_Text weightText;
    public AirplaneController flightController;

    void Start()
    {
        if (flightController == null)
        {
            flightController = FindObjectOfType<AirplaneController>();
            if (flightController == null)
            {
                Debug.LogError("No AirplaneController found in the scene!");
            }
        }


        // Set initial values for the sliders
        thrustSlider.value = flightController.thrust;
        weightSlider.value = flightController.weight;
        
        // Update the UI
        UpdateThrustText();
        UpdateWeightText();
    }

    void Update()
    {
        // Update the values in the flight controller
        flightController.thrust = thrustSlider.value;
        flightController.weight = weightSlider.value;

        // Update the displayed text
        UpdateThrustText();
        UpdateWeightText();
    }

    void UpdateThrustText()
    {
        thrustText.text = "Thrust: " + thrustSlider.value.ToString("F2");
    }

    void UpdateWeightText()
    {
        weightText.text = "Weight: " + weightSlider.value.ToString("F2");
    }
}
