
//4/12/16 15.03
package models;

public class User {
    private int id;
    private String username;
    private String experience;
    private String profileSettings;

    public User(int id, String username, String experience, String profileSettings) {
        this.id = id;
        this.username = username;
        this.experience = experience;
        this.profileSettings = profileSettings;
    }

    public int getId() {
        return id;
    }

    public String getUsername() {
        return username;
    }

    public String getExperience() {
        return experience;
    }

    public String getProfileSettings() {
        return profileSettings;
    }
}
