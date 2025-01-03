from flask import Flask, request, jsonify
import numpy as np
import pandas as pd
from sklearn.linear_model import LinearRegression

app = Flask(__name__)

# Initialize a simple model (just for this example)
model = LinearRegression()
model.fit([[30, 1000, 10], [25, 1500, 20]], [1, 2])  # Example training

@app.route('/predict', methods=['POST'])
def predict():
    # Receive weather data in JSON format
    data = request.json
    temperature = data['temperature']
    altitude = data['altitude']
    wind_speed = data['wind_speed']
    
    # Make a prediction based on the model
    prediction = model.predict([[temperature, altitude, wind_speed]])
    
    # Return the predicted turbulence level
    return jsonify({'turbulence': prediction[0]})

if __name__ == '__main__':
    app.run(debug=True)
