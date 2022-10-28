public interface GEGPlayerIF
{
    double health { get; set; } // Getter/Setter of player health
    double attackRate { get; set; } // Getter/Setter of player attck rate (APM)
    void PlayerKilled();
}
