from app import connect_db


def remove_course(student_id, course_id):
    conn = connect_db()
    if conn is None:
        return
    cursor = conn.cursor()
    cursor.execute("DELETE FROM Selections WHERE StudentID = %s AND CourseID = %s", (student_id, course_id))
    conn.commit()
    print("Course removed successfully!")
    conn.close()
