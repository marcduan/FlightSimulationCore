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

    # Apply Gaussian blur to reduce noise
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)

    # Detect circles using Hough Circle Transform
    circles = cv2.HoughCircles(
        blurred,
        cv2.HOUGH_GRADIENT,
        dp=1.2,
        minDist=100,  # Increase minDist to reduce overlapping circles
        param1=50,    # Adjust param1 for Canny edge detection
        param2=30,    # Adjust param2 for circle detection threshold
        minRadius=50, # Set minRadius based on expected size
        maxRadius=150 # Set maxRadius based on expected size
    )

    # If no circles are detected, return
    if circles is None:
        print("No circles detected.")
        return False

    # Process detected circles
    circles = np.uint16(np.around(circles))
    landing_pad_detected = False

    for i in circles[0, :]:
        center = (i[0], i[1])  # Center of the circle
        radius = i[2]          # Radius of the circle

        # Extract the ROI (Region of Interest) around the circle
        x1, y1 = max(0, center[0] - radius), max(0, center[1] - radius)
        x2, y2 = min(image.shape[1], center[0] + radius), min(image.shape[0], center[1] + radius)
        roi = gray[y1:y2, x1:x2]

        # Detect the "H" using contour detection or template matching
        _, binary = cv2.threshold(roi, 100, 255, cv2.THRESH_BINARY_INV)
        contours, _ = cv2.findContours(binary, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

        for contour in contours:
            approx = cv2.approxPolyDP(contour, 0.02 * cv2.arcLength(contour, True), True)
            x, y, w, h = cv2.boundingRect(approx)

            # Check for "H"-like shape
            aspect_ratio = w / float(h)
            if 0.3 < aspect_ratio < 0.7:  # Rough aspect ratio for "H"
                # Additional check for "H" shape
                if is_h_shape(binary[y:y+h, x:x+w]):
                    landing_pad_detected = True
                    cv2.rectangle(image, (x1 + x, y1 + y), (x1 + x + w, y1 + y + h), (0, 255, 0), 2)

        # Draw the detected circle
        cv2.circle(image, center, radius, (255, 0, 0), 2)

    # Display the result
    cv2.imshow("Landing Pad Detection", image)
    cv2.waitKey(0)
    cv2.destroyAllWindows()

    if landing_pad_detected:
        print("Landing pad detected!")
        return True
    else:
        print("No landing pad detected.")
        return False

def is_h_shape(roi):
    # Convert the ROI to the same type as the template
    roi = cv2.convertScaleAbs(roi)

    # Create a simple "H" template for matching
    h_template = np.array([[0, 255, 0], [255, 255, 255], [0, 255, 0]], dtype=np.uint8)

    # Ensure template size matches ROI size
    if roi.shape[0] < h_template.shape[0] or roi.shape[1] < h_template.shape[1]:
        return False

    # Resize the template to match the ROI size
    h_template_resized = cv2.resize(h_template, (roi.shape[1], roi.shape[0]))

    # Match the template
    match = cv2.matchTemplate(roi, h_template_resized, cv2.TM_CCOEFF_NORMED)
    _, max_val, _, _ = cv2.minMaxLoc(match)
    return max_val > 0.8

# Test the function
if __name__ == "__main__":
    image_path = "C:\\Users\\Duanm\\FlightSimulationCore\\ML_Predictions\\2022180163DIPProject\\LandingPadPictures\\Figure2.jpg"
    detected = detect_landing_pad(image_path)
"""

"""
import cv2
import numpy as np

def detect_landing_pad(image_path, min_radius=50, max_radius=150, min_dist=100, param1=50, param2=30):
    """ """
    Detects a landing pad in an image.

    Args:
        image_path: Path to the image file.
        min_radius: Minimum radius of the landing pad circle.
        max_radius: Maximum radius of the landing pad circle.
        min_dist: Minimum distance between detected circles.
        param1: First method-specific parameter for HoughCircles.
        param2: Second method-specific parameter for HoughCircles.

    Returns:
        True if a landing pad is detected, False otherwise.
"""

"""
    # Load the image
    image = cv2.imread(image_path)
    if image is None:
        print("Image not found!")
        return False

    # Convert to grayscale
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Apply Gaussian blur to reduce noise
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)

    # Detect circles using Hough Circle Transform
    circles = cv2.HoughCircles(
        blurred,
        cv2.HOUGH_GRADIENT,
        dp=1,
        minDist=min_dist,
        param1=param1,
        param2=param2,
        minRadius=min_radius,
        maxRadius=max_radius
    )

    # If no circles are detected, return False
    if circles is None:
        return False

    # Process detected circles
    circles = np.uint16(np.around(circles))
    for i in circles[0, :]:
        center = (i[0], i[1])  # Center of the circle
        radius = i[2]

        # Extract ROI around the circle
        x1, y1 = max(0, center[0] - radius), max(0, center[1] - radius)
        x2, y2 = min(image.shape[1], center[0] + radius), min(image.shape[0], center[1] + radius)
        roi = gray[y1:y2, x1:x2]

        # Enhance contrast for better shape detection
        clahe = cv2.createCLAHE(clipLimit=2.0, tileGridSize=(8, 8))
        roi_clahe = clahe.apply(roi)

        # Thresholding to create a binary image
        _, thresh = cv2.threshold(roi_clahe, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)

        # Find contours in the binary image
        contours, _ = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

        # Iterate through contours
        for contour in contours:
            # Approximate the contour to a polygon
            approx = cv2.approxPolyDP(contour, 0.04 * cv2.arcLength(contour, True), True)

            # Check for H-like shape based on number of vertices and area
            if len(approx) >= 4 and cv2.contourArea(approx) > 0.1 * (radius * radius * np.pi):
                return True

    return False

# Test the function
if __name__ == "__main__":
    image_path = "C:\\Users\\Duanm\\Pictures\\Screenshots\\Screenshot 2024-12-09 003526.png"
    detected = detect_landing_pad(image_path)
    print("Landing pad detected:", detected)
    """


import cv2
import numpy as np

def detect_landing_pad(image_path, min_radius=50, max_radius=150, min_dist=100, param1=100, param2=50):
    """
    Detects a landing pad in an image.

    Args:
        image_path: Path to the image file.
        min_radius: Minimum radius of the landing pad circle.
        max_radius: Maximum radius of the landing pad circle.
        min_dist: Minimum distance between detected circles.
        param1: First method-specific parameter for HoughCircles.
        param2: Second method-specific parameter for HoughCircles.

    Returns:
        True if a landing pad is detected, False otherwise.
    """

    # Load the image
    image = cv2.imread(image_path)
    if image is None:
        print("Image not found!")
        return False

    # Convert to grayscale
    gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Apply Gaussian blur to reduce noise
    blurred = cv2.GaussianBlur(gray, (5, 5), 0)

    # Detect circles using Hough Circle Transform
    circles = cv2.HoughCircles(
        blurred,
        cv2.HOUGH_GRADIENT,
        dp=1.2,
        minDist=min_dist,
        param1=param1,
        param2=param2,
        minRadius=min_radius,
        maxRadius=max_radius
    )

    # If no circles are detected, return False
    if circles is None:
        print("No circles detected.")
        return False

    # Process detected circles
    circles = np.uint16(np.around(circles))
    for i in circles[0, :]:
        center = (i[0], i[1])
        radius = i[2]

        # Extract ROI around the circle
        x1 = max(0, int(center[0]) - radius)
        y1 = max(0, int(center[1]) - radius)
        x2 = min(image.shape[1], int(center[0]) + radius)
        y2 = min(image.shape[0], int(center[1]) + radius)
        roi = gray[y1:y2, x1:x2]

        # Enhance contrast for better shape detection
        clahe = cv2.createCLAHE(clipLimit=2.0, tileGridSize=(8, 8))
        roi_clahe = clahe.apply(roi)

        # Thresholding to create a binary image
        _, thresh = cv2.threshold(roi_clahe, 0, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)

        # Find contours in the binary image
        contours, _ = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

        for contour in contours:
            approx = cv2.approxPolyDP(contour, 0.04 * cv2.arcLength(contour, True), True)

            # Check for H-like shape based on vertices and aspect ratio
            if len(approx) >= 4 and cv2.contourArea(approx) > 0.1 * (radius ** 2 * np.pi) and is_h_like_shape(approx):
                cv2.circle(image, center, radius, (0, 255, 0), 2)
                cv2.rectangle(image, (x1, y1), (x2, y2), (255, 0, 0), 2)
                cv2.putText(image, "Landing Pad Detected", (x1, y1 - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 255, 0), 2)
                cv2.imshow("Landing Pad Detection", image)
                cv2.waitKey(0)
                cv2.destroyAllWindows()
                return True

    print("No landing pad detected.")
    return False

def is_h_like_shape(approx):
    """
    Checks if the detected contour resembles an "H" shape.
    """
    x, y, w, h = cv2.boundingRect(approx)
    aspect_ratio = w / float(h)

    # Ensure the aspect ratio roughly matches an "H"
    if 0.3 < aspect_ratio < 0.7:
        return True
    return False

# Test the function
if __name__ == "__main__":
    image_path = "C:\\Users\\Duanm\\Desktop\\3rd_year\\Human-computer Interaction 人机交互\\own\\P6(Final Part)_段瑞豪2022180163\\enhancedLandingPictures\\Figure_2.png"# Update with your image path
    detected = detect_landing_pad(image_path)
    print("Landing pad detected:", detected) 
with open("detection_status.txt", "w") as f:
    f.write("Landing pad detected" if detected else "No pad detected")
