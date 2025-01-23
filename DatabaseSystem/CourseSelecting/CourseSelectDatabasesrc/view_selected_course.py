from app import connect_db

def view_selected_courses(student_id):
    conn = connect_db()
    if conn is None:
        return
    
    cursor = conn.cursor()
    try:
        # Query to get selected courses for the student
        cursor.execute("""
            SELECT c.CourseID, c.CourseName, c.Credits, c.TimeSlot
            FROM Selections s
            JOIN Courses c ON s.CourseID = c.CourseID
            WHERE s.StudentID = %s
        """, (student_id,))
        
        selected_courses = cursor.fetchall()
        
        # Display the selected courses
        if not selected_courses:
            print(f"No courses selected for Student ID {student_id}.")
        else:
            print(f"\nCourses Selected by Student ID {student_id}:")
            for course in selected_courses:
                print(f"ID: {course[0]}, Name: {course[1]}, Credits: {course[2]}, Time: {course[3]}")
    except Exception as e:
        print(f"Error: {e}")
    finally:
        conn.close()

# Call the function to test it
if __name__ == "__main__":
    student_id = int(input("Enter Student ID to view selected courses: "))
    view_selected_courses(student_id)
