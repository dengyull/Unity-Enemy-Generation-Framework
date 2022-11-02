/// <summary>
/// This interface should be implemented by the class that control enemies' behaviors
/// </summary>
public interface GEGEnemyIF {
    double health { get; set; } // Getter/Setter of enemy health
    double attackRate { get; set; } // Getter/Setter of enemy attck rate (APM)
    void EnemyKilled(); // called when the enemy get killed 
}
