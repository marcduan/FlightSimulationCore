from flask import Flask, request, jsonify
import numpy as np
import pandas as pd
from sklearn.linear_model import LinearRegression

app = Flask(__name__)

# Load your trained machine learning model (can be saved with pickle)
model = LinearRegression()
# Sample model: load your trained model or retrain here
model.fit([[30, 1000, 10], [25, 1500, 20]], [1, 2])  # Example training

@app.route('/predict', methods=['POST'])
def predict():
    data = request.json
    temperature = data['temperature']
    altitude = data['altitude']
    wind_speed = data['wind_speed']
    
    # Make prediction
    prediction = model.predict([[temperature, altitude, wind_speed]])
    
    return jsonify({'turbulence':  prediction[0]})

if __name__ == '__main__':
    app.run(debug=True)

