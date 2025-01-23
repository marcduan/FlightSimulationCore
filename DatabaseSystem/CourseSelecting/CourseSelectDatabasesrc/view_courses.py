from app import connect_db


def view_courses():
    conn = connect_db()
    if conn is None:
        print("Failed to connect to the database.")
        return
    cursor = conn.cursor()
    print("Fetching courses...")
    
    try:
        cursor.execute("SELECT * FROM Courses")
        courses = cursor.fetchall()
        print("Data fetched successfully!")
        
        if not courses:
            print("No courses available in the database.")
            return
        
        print("\nAvailable Courses:")
        for course in courses:
            print(f"ID: {course[0]}, Name: {course[1]}, Credits: {course[2]}, Time: {course[4]}")
    except Exception as e:
        print(f"Error fetching data: {e}")
    finally:
        conn.close()
        print("Database connection closed.")
