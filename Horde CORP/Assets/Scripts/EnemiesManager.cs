using System.Collections.Generic;
using UnityEngine;
using FMODUnity; // Добавляем для работы с FMOD

public class EnemiesManager : MonoBehaviour
{
    #region Singleton
    static EnemiesManager _i; 
    public static EnemiesManager i
    {
        get
        {
            if(_i == null) _i = GameObject.FindObjectOfType<EnemiesManager>();
            return _i;
        }
    }
    #endregion

    public List<Transform> enemies = new List<Transform>();
    public List<EnemySpawning> unlockable;

    [Header("FMOD Settings")]
    [SerializeField] private StudioEventEmitter musicEmitter; // Эмиттер музыки FMOD
    private int lastEnemyCount; // Запоминаем последнее количество, чтобы не проверять каждый кадр

    private void Update()
    {
        // Проверяем только если количество изменилось
        if (enemies.Count != lastEnemyCount)
        {
            UpdateFMODThreatLevel();
            lastEnemyCount = enemies.Count;
        }
    }

    private void UpdateFMODThreatLevel()
    {
        float threatLevel = (enemies.Count >= 5) ? 1f : 0f; // Threat = 1, если врагов ≥ 15
        
        if (musicEmitter != null)
        {
            musicEmitter.SetParameter("Threat", threatLevel); // Меняем параметр в FMOD
        }
        else
        {
            Debug.LogWarning("FMOD Music Emitter не назначен в EnemiesManager!");
        }
    }
}