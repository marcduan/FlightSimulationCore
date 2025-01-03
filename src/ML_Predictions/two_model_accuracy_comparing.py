# Import required libraries
import numpy as np
import matplotlib.pyplot as plt
from sklearn.model_selection import train_test_split
from sklearn.ensemble import RandomForestClassifier
from sklearn.svm import SVC
from sklearn.metrics import accuracy_score, ConfusionMatrixDisplay
import pandas as pd

# Load the dataset
df = pd.read_csv("C:\\Users\\Duanm\\Downloads\\flight_data_intervals2.csv")

# Split features and labels
X = df.drop(columns=["turbulence"])  # Features
y = df["turbulence"]                # Labels

# Split the dataset
X_train, X_test, y_train, y_test = train_test_split(X, y, test_size=0.2, random_state=42)

# Train the Random Forest Classifier
rfc = RandomForestClassifier(random_state=42)
rfc.fit(X_train, y_train)
rfc_predictions = rfc.predict(X_test)

# Train the Support Vector Machine
from sklearn.preprocessing import StandardScaler
scaler = StandardScaler()
X_train_scaled = scaler.fit_transform(X_train)
X_test_scaled = scaler.transform(X_test)

svm = SVC(probability=True, random_state=42)
svm.fit(X_train_scaled, y_train)
svm_predictions = svm.predict(X_test_scaled)

# Calculate accuracy
rfc_accuracy = accuracy_score(y_test, rfc_predictions)
svm_accuracy = accuracy_score(y_test, svm_predictions)

# Display Confusion Matrices
ConfusionMatrixDisplay.from_predictions(y_test, rfc_predictions, display_labels=np.unique(y))
plt.title("Random Forest Confusion Matrix")
plt.show()

ConfusionMatrixDisplay.from_predictions(y_test, svm_predictions, display_labels=np.unique(y))
plt.title("SVM Confusion Matrix")
plt.show()

# Visualize Model Accuracy
model_names = ['Random Forest', 'SVM']
accuracies = [rfc_accuracy, svm_accuracy]

plt.bar(model_names, accuracies, color=['green', 'blue'])
plt.title("Model Accuracy Comparison")
plt.ylabel("Accuracy")
plt.ylim(0, 1)  # Accuracy ranges between 0 and 1
plt.show()
