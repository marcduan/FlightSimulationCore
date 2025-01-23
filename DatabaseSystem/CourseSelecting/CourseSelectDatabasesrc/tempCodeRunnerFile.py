")

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
            student_id = int(input("