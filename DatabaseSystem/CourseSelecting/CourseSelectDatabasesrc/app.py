"""
def main():
    while True:
        print("\n1. View Courses")
        print("2. Add Course")
        print("3. Remove Course")
        print("4. Exit")
        choice = input("Enter your choice: ")

        if choice == '1':
            view_courses()
        elif choice == '2':
            student_id = int(input("Enter your Student ID: "))
            course_id = int(input("Enter the Course ID to add: "))
            add_course(student_id, course_id)
        elif choice == '3':
            student_id = int(input("Enter your Student ID: "))
            course_id = int(input("Enter the Course ID to remove: "))
            remove_course(student_id, course_id)
        elif choice == '4':
            print("Exiting the system. Goodbye!")
            break
        else:
            print("Invalid choice. Please try again.")

if __name__ == "__main__":
    main()
"""

import mysql.connector

def connect_db():
    try:
        conn = mysql.connector.connect(
            host="localhost",  # Your MySQL server address
            user="root",       # Your MySQL username
            password="123",  # Your MySQL password
            database="course_system"  # Database name
        )
        print("Database connected successfully!")
        return conn
    except mysql.connector.Error as err:
        print(f"Error: {err}")
        return None

# Test the connection
if __name__ == "__main__":
    connect_db()
