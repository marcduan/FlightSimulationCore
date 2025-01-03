
package dao;

import java.sql.ResultSet;
import db.DatabaseManager;
import models.FlightLog;
import java.sql.SQLException;
import java.util.List;

import java.sql.PreparedStatement;

import java.sql.Timestamp;
import java.util.ArrayList;

public class FlightLogDAO {
  private final DatabaseManager dbManager;

  public FlightLogDAO(DatabaseManager dbManager) {
    this.dbManager = dbManager;
  }

  public int addFlightLog(FlightLog log) throws SQLException {
      String sql = "INSERT INTO FlightLogs (user_id, timestamp, altitude, speed, fuel_level) VALUES (?, ?, ?, ?, ?)";
      try (PreparedStatement stmt = dbManager.getConnection().prepareStatement(sql)) {
          stmt.setInt(1, log.getUserId());
          stmt.setTimestamp(2, log.getTimestamp());
          stmt.setDouble(3, log.getAltitude());
          stmt.setDouble(4, log.getSpeed());
          stmt.setDouble(5, log.getFuelLevel());
          return stmt.executeUpdate();
        }
    }

    public List<FlightLog> getAllFlightLogs() throws SQLException {
        String sql = "SELECT * FROM FlightLogs";
        try (PreparedStatement stmt = dbManager.getConnection().prepareStatement(sql)) {
            try (ResultSet rs = stmt.executeQuery()) {
                List<FlightLog> flights = new ArrayList<>();
                while (rs.next()) {
                    int flightId = rs.getInt("flight_id");
                    int userId = rs.getInt("user_id");
                    Timestamp timestamp = rs.getTimestamp("timestamp");
                    double altitude = rs.getDouble("altitude");
                    double speed = rs.getDouble("speed");
                    double fuelLevel = rs.getDouble("fuel_level");
                    flights.add(new FlightLog(flightId, userId, timestamp, altitude, speed, fuelLevel));
                }
                return flights;
            }
        }
    }
}