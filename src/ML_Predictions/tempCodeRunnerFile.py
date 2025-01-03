import cv2
import numpy as np

# Load the image
image = cv2.imread("C:\\Users\\Duanm\\Pictures\\Screenshots\\eVtolLandingPad.png")

# Convert the image to grayscale
gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

# Apply GaussianBlur to reduce noise
blurred = cv2.GaussianBlur(gray, (5, 5), 0)

# Perform edge detection
edges = cv2.Canny(blurred, 50, 150)

# Find contours in the image
contours, _ = cv2.findContours(edges, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

# Draw contours and detect circular shapes
for contour in contours:
    perimeter = cv2.arcLength(contour, True)
    approx = cv2.approxPolyDP(contour, 0.02 * perimeter, True)
    area = cv2.contourArea(contour)

    # Check if the shape is circular-like and large enough
    if len(approx) > 8 and area > 500:
        cv2.drawContours(image, [approx], -1, (0, 255, 0), 3)

# Show the output image
cv2.imshow("Landing Pad Detection", image)
cv2.waitKey(0)
cv2.destroyAllWindows()

