from flask import Flask, request, jsonify
from flight_logger import log_flight_data

app = Flask(__name__)

@app.route('/log-flight', methods=['POST'])
def log_flight():
    data = request.json
    log_flight_data(
        time=data["time"],
        speed=data["speed"],
        altitude=data["altitude"],
        fuel_level=data["fuel_level"],
        weather_condition=data["weather_condition"]
    )
    return jsonify({"message": "Flight data logged successfully!"}), 200

if __name__ == '__main__':
    app.run(debug=True)

