package dao;

import db.DatabaseManager;
import models.User;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;

public class UserDAO {
    private DatabaseManager dbManager;

    public UserDAO(DatabaseManager dbManager) {
        this.dbManager = dbManager;
    }

    // CREATE: Add a new user
    public void addUser(User user) {
        String query = "INSERT INTO Users (username, experience, profile_settings) VALUES (?, ?, ?)";
        try (Connection conn = dbManager.getConnection();
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setString(1, user.getUsername());
            stmt.setString(2, user.getExperience());
            stmt.setString(3, user.getProfileSettings());
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    // READ: Retrieve all users
    public List<User> getAllUsers() throws SQLException {
        String sql = "SELECT * FROM Users";
        try (PreparedStatement stmt = dbManager.getConnection().prepareStatement(sql);
             ResultSet rs = stmt.executeQuery()) {

            List<User> users = new ArrayList<>();
            while (rs.next()) {
                int id = rs.getInt("id");
                String username = rs.getString("username");
                //String password = rs.getString("password");
                String experience = rs.getString("experience");
                String settings = rs.getString("profile_settings");
                users.add(new User(id, username, experience, settings));
            }
            return users;
        }
    }


    // UPDATE: Update a user's experience level
    /*
    public void updateUserExperience(int userId, String newExperience) {
        String query = "UPDATE Users SET experience = ? WHERE id = ?";
        try (Connection conn = dbManager.getConnection();
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setString(1, newExperience);
            stmt.setInt(2, userId);
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
    */
    public void updateUserExperience(int userId, String newExperience) {
        String query = "UPDATE Users SET experience = ? WHERE id = ?"; // Verify 'experience'
        try (Connection conn = dbManager.getConnection();
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setString(1, newExperience); // Set the new experience level
            stmt.setInt(2, userId);           // Specify user ID
            int rowsUpdated = stmt.executeUpdate();

            if (rowsUpdated > 0) {
                System.out.println("User experience updated successfully.");
            } else {
                System.out.println("No user found with the given ID.");
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }





    // DELETE: Delete a user by ID
    public void deleteUser(int userId) {
        String query = "DELETE FROM Users WHERE id = ?";
        try (Connection conn = dbManager.getConnection();
             PreparedStatement stmt = conn.prepareStatement(query)) {

            stmt.setInt(1, userId);
            stmt.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}