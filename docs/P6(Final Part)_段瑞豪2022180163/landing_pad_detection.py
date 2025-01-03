

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
    image_path = "C:\\Users\\Duanm\\FlightSimulationCore\\ML_Predictions\\enhancedLandingPictures\\Figure_2.png"# Update with your image path
    detected = detect_landing_pad(image_path)
    print("Landing pad detected:", detected) 
with open("detection_status.txt", "w") as f:
    f.write("Landing pad detected" if detected else "No pad detected")
