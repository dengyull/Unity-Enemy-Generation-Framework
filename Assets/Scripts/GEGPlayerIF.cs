/// <summary>
/// This interface should be implemented by the class that control players' behaviors
/// </summary>
public interface GEGPlayerIF
{
    double health { get; set; } // Getter/Setter of player health
    double attackRate { get; set; } // Getter/Setter of player attck rate (APM)
    void PlayerKilled(); // called when player get killed
}
