using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private PlayerBehaviour _player;
    public PlayerBehaviour Player 
    { 
        get { return _player; } 
        set { _player = value; }
    }

    private List<EnemyBehaviour> _enemies = new();

    public void AddEnemy(EnemyBehaviour enemy)
    {
        if (!_enemies.Contains(enemy))
        {
            _enemies.Add(enemy);
        }
    }

    public void RemoveEnemy(EnemyBehaviour enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    public void ClearEnemyList()
    {
        _enemies.Clear();
    }

    private List<Transform> _agentNodes = new();
    public List<Transform> AgentNodes 
    { 
        get { return _agentNodes; } 
        set { _agentNodes = value; }
    }
}
