from app import connect_db
from check_prerequisite import check_prerequisite


def check_constraints(student_id, course_id):
    conn = connect_db()
    cursor = conn.cursor()

    # Get credits of the new course
    cursor.execute("SELECT Credits, TimeSlot FROM Courses WHERE CourseID = %s", (course_id,))
    course_data = cursor.fetchone()
    if not course_data:
        print("Invalid Course ID.")
        return False
    
    new_course_credits, new_course_time = course_data

    # Check total credits
    cursor.execute("""
        SELECT SUM(Credits) 
        FROM Courses 
        JOIN Selections ON Courses.CourseID = Selections.CourseID
        WHERE StudentID = %s
    """, (student_id,))
    total_credits = cursor.fetchone()[0] or 0

    if total_credits + new_course_credits > 21:
        print("Error: Credit limit of 21 exceeded!")
        return False

    # Check for time conflicts
    cursor.execute("""
        SELECT TimeSlot 
        FROM Courses 
        JOIN Selections ON Courses.CourseID = Selections.CourseID
        WHERE StudentID = %s
    """, (student_id,))
    selected_times = [row[0] for row in cursor.fetchall()]
    
    if new_course_time in selected_times:
        print("Error: Time conflict detected!")
        return False
    
    if not check_prerequisite(student_id, course_id):
        return False

    conn.close()
    return True

# Call this function before adding a course
