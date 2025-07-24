using UnityEngine;
public enum MiniGameType
{
    Bottles,
    Hanoi,
    Ferry,
    Knight
}

[CreateAssetMenu(fileName = "LevelData", menuName = "EduQuest/Level", order = 0)]
public class LevelData : ScriptableObject
{
    [Header("Базовые")]
    public string levelId = "level_01";
    public string displayName = "Перелив 3‑5";
    public MiniGameType gameType;

    [Header("Пороги звёзд (секунды или ходы)")]
    public int goldThreshold = 60; 
    public int silverThreshold = 120;

    [Header("Bottles (Перелив)")]
    public int[] bottleVolumes; 
    public int targetVolume;   

    [Header("Hanoi (Диски)")]
    public int hanoiDiskCount = 3;

    [Header("Knight (Конюх)")]
    [Range(4,6)] public int knightCount = 6; 

    [Header("Ferry (Паромщик)")]
    public bool useWolfGoatCabbage = true;

    public int EvaluateStars(int metricValue)
    {
        if (metricValue <= goldThreshold) return 3;
        if (metricValue <= silverThreshold) return 2;
        return 1;
    }
}
