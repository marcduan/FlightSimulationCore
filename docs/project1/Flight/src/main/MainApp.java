
package main;

import dao.UserDAO;
import db.DatabaseManager;
import models.User;

import java.sql.SQLException;
import java.util.Scanner;

public class MainApp {
    public static void main(String[] args) throws SQLException {
        // Initialize resources outside the loop
        DatabaseManager dbManager = null;
        UserDAO userDAO = null;

        try {
            dbManager = new DatabaseManager("jdbc:mysql://localhost:3306/FlightSimulationDB", "root", "Abcdefgh1234$");
            userDAO = new UserDAO(dbManager);

            Scanner scanner = new Scanner(System.in);
            boolean running = true;

            while (running) {
                System.out.println("\n--- Flight Simulation Database ---");
                System.out.println("1. Add User");
                System.out.println("2. View All Users");
                System.out.println("3. Update User Experience Level");
                System.out.println("4. Delete User");
                System.out.println("5. Exit");
                System.out.print("Choose an option: ");

                if (!scanner.hasNextInt()) {
                    System.out.println("Invalid input. Please enter a number between 1 and 5.");
                    scanner.nextLine(); // Clear invalid input
                    continue;
                }

                int choice = scanner.nextInt();
                scanner.nextLine(); // Clear buffer after reading int

                switch (choice) {
                    case 1:
                        // Add User
                        System.out.print("Enter username: ");
                        String username = scanner.nextLine();

                        System.out.print("Enter experience level: ");
                        String experience = scanner.nextLine();

                        System.out.print("Enter profile settings: ");
                        String profileSettings = scanner.nextLine();

                        userDAO.addUser(new User(0, username, experience, profileSettings));
                        System.out.println("User added successfully.");
                        break;

                    case 2:
                        // View All Users
                        System.out.println("\n--- All Users ---");
                        userDAO.getAllUsers().forEach(user -> {
                            System.out.println("ID: " + user.getId());
                            System.out.println("Username: " + user.getUsername());
                            System.out.println("Experience: " + user.getExperience());
                            System.out.println("Profile Settings: " + user.getProfileSettings());
                            System.out.println("-----------------------");
                        });
                        break;

                    case 3:
                        // Update User Experience
                        System.out.print("Enter user ID: ");
                        if (!scanner.hasNextInt()) {
                            System.out.println("Invalid input. User ID must be a number.");
                            scanner.nextLine(); // Clear invalid input
                            break;
                        }
                        int userId = scanner.nextInt();
                        scanner.nextLine(); // Clear buffer

                        System.out.print("Enter new experience level: ");
                        String newExperience = scanner.nextLine();

                        userDAO.updateUserExperience(userId, newExperience);
                        System.out.println("User experience updated successfully.");
                        break;

                    case 4:
                        // Delete User
                        System.out.print("Enter user ID to delete: ");
                        if (!scanner.hasNextInt()) {
                            System.out.println("Invalid input. User ID must be a number.");
                            scanner.nextLine(); // Clear invalid input
                            break;
                        }
                        int deleteId = scanner.nextInt();
                        scanner.nextLine(); // Clear buffer

                        userDAO.deleteUser(deleteId);
                        System.out.println("User deleted successfully.");
                        break;

                    case 5:
                        // Exit Program
                        running = false;
                        System.out.println("Exiting... Goodbye!");
                        break;

                    default:
                        System.out.println("Invalid choice. Please enter a number between 1 and 5.");
                }
            }
        } catch (Exception e) {
            System.out.println("An error occurred: " + e.getMessage());
            e.printStackTrace();
        } finally {
            // Clean up resources
            if (dbManager != null) {
                dbManager.closeConnection();
            }
            System.out.println("Database connection closed.");
        }
    }
}

