from app import connect_db


def check_prerequisite(student_id, course_id):
    conn = connect_db()
    if conn is None:
        return False
    
    cursor = conn.cursor()
    try:
        # Fetch the prerequisite for the course
        cursor.execute("SELECT Prerequisite FROM Courses WHERE CourseID = %s", (course_id,))
        result = cursor.fetchone()
        
        if result is None:
            print("Error: Course ID not found.")
            return False
        
        prerequisite = result[0]  # Extract prerequisite value

        # If no prerequisite, course can be taken
        if prerequisite is None:
            return True

        # Check if the student has already selected (or completed) the prerequisite
        cursor.execute("""
            SELECT COUNT(*) 
            FROM Selections 
            WHERE StudentID = %s AND CourseID = %s
        """, (student_id, prerequisite))
        count = cursor.fetchone()[0]

        if count == 0:
            print("Prerequisite not satisfied. You need to complete Course ID:", prerequisite)
            return False

        return True
    except Exception as e:
        print(f"Error checking prerequisite: {e}")
        return False
    finally:
        conn.close()
