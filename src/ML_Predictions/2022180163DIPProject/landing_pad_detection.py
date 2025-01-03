"""

import cv2
import numpy as np
import matplotlib.pyplot as plt

# Load the landing pad texture
image = cv2.imread("C:\\Users\\Duanm\\FlightSimulationCore\\ML_Predictions\\LandingPadPictures\\Figure4.jpg")

# Check if the image loaded correctly
if image is None:
    raise ValueError("Image not found or could not be loaded.")

# Step 1: Convert to grayscale
gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

# Step 2: Apply GaussianBlur to soften
blurred = cv2.GaussianBlur(gray, (5, 5), 0)

# Step 3: Use edge detection to highlight markings
edges = cv2.Canny(blurred, 50, 150)

# Step 4: Overlay edges on the original texture
edges_colored = cv2.cvtColor(edges, cv2.COLOR_GRAY2BGR)  # Convert edges to BGR
enhanced_texture = cv2.addWeighted(image, 0.8, edges_colored, 0.2, 0)

# Save the enhanced texture
cv2.imwrite("enhanced_landing_pad.png", enhanced_texture)

# Display the result
plt.imshow(cv2.cvtColor(enhanced_texture, cv2.COLOR_BGR2RGB))
plt.title("Enhanced Landing Pad Texture")
plt.axis("off")
plt.show()
"""




import cv2
import numpy as np

def detect_landing_pad(image_path):
    # Load the image
    image = cv2.imread(image_path)
    if image is None:
        print("Image not found!")
        return False

    # Convert to grayscale
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Detect circles using Hough Circle Transform
    circles = cv2.HoughCircles(
        gray, 
        cv2.HOUGH_GRADIENT, 
        dp=1.2, 
        minDist=30, 
        param1=50, 
        param2=30, 
        minRadius=20, 
        maxRadius=100
    )

    # Visualize the detection
    if circles is not None:
        circles = np.uint16(np.around(circles))
        for i in circles[0, :]:
            center = (i[0], i[1])  # Center of the circle
            radius = i[2]         # Radius of the circle
            cv2.circle(image, center, radius, (0, 255, 0), 2)  # Draw circle
            cv2.circle(image, center, 2, (0, 0, 255), 3)      # Draw center

        cv2.imshow("Landing Pad Detection", image)
        cv2.waitKey(0)
        cv2.destroyAllWindows()
        print("Landing pad detected!")
        return True
    else:
        print("No landing pad detected.")
        return False

# Test the function
if __name__ == "__main__":
    image_path = ""C:\Users\Duanm\FlightSimulationCore\ML_Predictions\\2022180163???DIPProject\\LandingPadPictures\\Figure1.jpeg"  # Replace with your actual image path
    detected = detect_landing_pad(image_path)

with open("detection_status.txt", "w") as f:
    f.write("Landing pad detected" if detected else "No pad detected")
