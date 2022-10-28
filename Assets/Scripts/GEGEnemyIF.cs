public interface GEGEnemyIF
{
    double health { get; set; } // Getter/Setter of enemy health
    double attackRate { get; set; } // Getter/Setter of enemy attck rate (APM)
    void EnemyKilled();
}
