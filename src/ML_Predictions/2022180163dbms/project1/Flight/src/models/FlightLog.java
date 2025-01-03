package models;

import java.sql.Timestamp;

public class FlightLog {
    private int flightId;
    private int userId;
    private Timestamp timestamp;
    private double altitude;
    private double speed;
    private double fuelLevel;

    public FlightLog(int flightId, int userId, Timestamp timestamp, double altitude, double speed, double fuelLevel) {
        this.flightId = flightId;
        this.userId = userId;
        this.timestamp = timestamp;
        this.altitude = altitude;
        this.speed = speed;
        this.fuelLevel = fuelLevel;
    }

    public int getFlightId() {
        return flightId;
    }

    public int getUserId() {
        return userId;
    }

    public Timestamp getTimestamp() {
        return timestamp;
    }

    public double getAltitude() {
        return altitude;
    }

    public double getSpeed() {
        return speed;
    }

    public double getFuelLevel() {
        return fuelLevel;
    }
}
