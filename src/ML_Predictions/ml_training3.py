import pandas as pd
from sklearn.model_selection import train_test_split
from sklearn.preprocessing import StandardScaler
from sklearn.ensemble import RandomForestClassifier
from sklearn.svm import SVC
from sklearn.metrics import accuracy_score, classification_report, confusion_matrix
import dlib

# Load the flight data
data = pd.read_csv("C:\\Users\\Duanm\\FlightSimulationCore\\src\\Assets\\Data\\flight_data.csv")
data = data.dropna()  # Remove missing values

# Inspect the dataset
print("Columns in the dataset:", data.columns)
print(data.head())

# Features (X) and Target (y)
X = data[['Speed', 'Altitude', 'FuelLevel']]
y = data['WeatherCondition']

# Split the data
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Scale data for SVM
scaler = StandardScaler()
X_train_scaled = scaler.fit_transform(X_train)
X_test_scaled = scaler.transform(X_test)

# Random Forest Classifier
rfc = RandomForestClassifier(n_estimators=100, random_state=42)
rfc.fit(X_train, y_train)
rfc_predictions = rfc.predict(X_test)

# Support Vector Machine
svm = SVC(kernel='rbf', C=1.0, gamma='scale', random_state=42)
svm.fit(X_train_scaled, y_train)
svm_predictions = svm.predict(X_test_scaled)

# Performance Evaluation
rfc_accuracy = accuracy_score(y_test, rfc_predictions)
svm_accuracy = accuracy_score(y_test, svm_predictions)

print("Random Forest Classifier Accuracy:", rfc_accuracy)
print("Support Vector Machine Accuracy:", svm_accuracy)

# Detailed Classification Reports
print("\nRandom Forest Classification Report:\n", classification_report(y_test, rfc_predictions))
print("\nSVM Classification Report:\n", classification_report(y_test, svm_predictions))

# Confusion Matrices
print("\nRandom Forest Confusion Matrix:\n", confusion_matrix(y_test, rfc_predictions))
print("\nSVM Confusion Matrix:\n", confusion_matrix(y_test, svm_predictions))
print("dlib succesfully installed")
















                            