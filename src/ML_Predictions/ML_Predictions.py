
import numpy as np
import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.linear_model import LinearRegression
from sklearn.metrics import mean_squared_error

# Sample data: temperature, altitude, wind speed -> turbulence level
data = {
    'temperature': [30, 25, 35, 40, 28, 22],
    'altitude': [1000, 1500, 1200, 1800, 900, 800],
    'wind_speed': [10, 20, 15, 25, 12, 8],
    'turbulence': [1, 2, 2, 3, 1, 0]  # 1: Low, 2: Medium, 3: High
}

# Create a DataFrame
df = pd.DataFrame(data)

# Features and target variable
X = df[['temperature', 'altitude', 'wind_speed']]
y = df['turbulence']

# Split data into train and test sets
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Initialize and train the model
model = LinearRegression()
model.fit(X_train, y_train)

# Make predictions
y_pred = model.predict(X_test)                  

# Evaluate the model
mse = mean_squared_error(y_test, y_pred)
print(f'Mean Squared Error: {mse}')











