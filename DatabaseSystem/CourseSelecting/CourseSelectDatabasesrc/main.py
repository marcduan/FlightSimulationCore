


from add_course import add_course
from check_prerequisite import check_prerequisite
from remove_course import remove_course
from view_courses import view_courses
from view_selected_course import view_selected_courses


def main():
    while True:
        print("\n1. View Courses")
        print("2. Add Course")
        print("3. Remove Course")
        print("4. Exit")
        print("5. View Selected Courses")
        choice = input("Enter your choice: ")

        if choice == '1':
            view_courses()
        elif choice == '2':
            student_id = int(input("Enter your Student ID: "))
            course_id = int(input("Enter the Course ID to add: "))
            if check_prerequisite(student_id, course_id):
                add_course(student_id, course_id)
            else:
                print("Course cannot be added due to prerequisite requirements.")
        elif choice == '3':
            student_id = int(input("Enter your Student ID: "))
            course_id = int(input("Enter the Course ID to remove: "))
            remove_course(student_id, course_id)
        elif choice == '4':
            print("Exiting the system. Goodbye!")
            break
        elif choice == '5':
            student_id = int(input("Enter your Student ID: "))
            view_selected_courses(student_id)
        else:
            print("Invalid choice. Please try again.")

if __name__ == "__main__":
    main()
