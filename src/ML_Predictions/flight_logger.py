


import os
import csv

# Function to log flight data
def log_flight_data(time, speed, altitude, fuel_level, weather_condition):
    file_path = "flight_data.csv"

    # Check if the file exists
    file_exists = os.path.exists(file_path)

    # Open the file in append mode
    with open(file_path, mode="a", newline="") as file:
        writer = csv.DictWriter(
            file,
            fieldnames=["time", "speed", "altitude", "fuellevel", "weathercondition"]
        )
        
        # Write header only if the file is new
        if not file_exists:
            writer.writeheader()

        # Write the flight data
        writer.writerow({
            "time": time,
            "speed": speed,
            "altitude": altitude,
            "fuellevel": fuel_level,
            "weathercondition": weather_condition
        })

# Example: Logging some data
if __name__ == "__main__":
    log_flight_data("12:00:00", 300, 15000, 80, "clear")
    log_flight_data("12:01:00", 320, 15500, 78, "partly cloudy")
    log_flight_data("12:02:00", 310, 15200, 76, "clear")

    print("Flight data logged successfully!")

















































