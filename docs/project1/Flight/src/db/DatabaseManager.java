
//24/12/16 20.00

package db;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.function.Function;

public class DatabaseManager {
    private final String url;
    private final String username;
    private final String password;
    private Connection connection;

    // Constructor to initialize connection parameters and establish the initial connection
    public DatabaseManager(String url, String username, String password) throws SQLException {
        this.url = url;
        this.username = username;
        this.password = password;
        this.connection = DriverManager.getConnection(url, username, password);
        System.out.println("Connected to the database!");
    }

    // Get the database connection; re-establish if closed
    public Connection getConnection() throws SQLException {
        if (connection == null || connection.isClosed()) {
            this.connection = DriverManager.getConnection(url, username, password);
            System.out.println("Re-established database connection.");
        }
        return this.connection;
    }

    // Execute an UPDATE/INSERT/DELETE query
    public int executeUpdate(String sql, Object... params) throws SQLException {
        try (PreparedStatement stmt = getConnection().prepareStatement(sql)) {
            setParameters(stmt, params);
            return stmt.executeUpdate();
        }
    }

    // Execute a SELECT query and map the result to a list
    public <T> List<T> executeQuery(String sql, Function<ResultSet, T> mapper, Object... params) throws SQLException {
        List<T> results = new ArrayList<>();
        try (PreparedStatement stmt = getConnection().prepareStatement(sql)) {
            setParameters(stmt, params);
            try (ResultSet rs = stmt.executeQuery()) {
                while (rs.next()) {
                    results.add(mapper.apply(rs));
                }
            }
        }
        return results;
    }

    // Helper method to set query parameters
    private void setParameters(PreparedStatement stmt, Object... params) throws SQLException {
        for (int i = 0; i < params.length; i++) {
            stmt.setObject(i + 1, params[i]);
        }
    }

    // Close the database connection
    public void closeConnection() throws SQLException {
        if (connection != null && !connection.isClosed()) {
            connection.close();
            System.out.println("Database connection closed.");
        }
    }
}
