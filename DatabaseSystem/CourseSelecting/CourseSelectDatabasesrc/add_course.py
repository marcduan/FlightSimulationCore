from app import connect_db
from check_constraints import check_constraints


def add_course(student_id, course_id):
    if check_constraints(student_id, course_id):
        conn = connect_db()
        if conn is None:
            return
        cursor = conn.cursor()
        cursor.execute("INSERT INTO Selections (StudentID, CourseID) VALUES (%s, %s)", (student_id, course_id))
        conn.commit()
        print("Course added successfully!")
        conn.close()