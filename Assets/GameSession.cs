using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
    private static GameSession _instance;
   
    public static GameSession Instance
    {
        get
        {
            if (!_instance)
            {

                _instance = FindObjectOfType<GameSession>();

            }

            return _instance;
        }
    }
    void Awake()
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public event Action<float> HealthChanged;
    private float _health = 1;
    public float Health
    {
        get { return _health; }
        set
        {
            if (Health <= 0)
            {
                _isDied = true;
            }
            _health = value;
            HealthChanged?.Invoke(_health);
        
        }
    }
    private bool _isDied = false;
    public bool IsDied
    {
        get { return _isDied; }
        set
        {
      
            _isDied = value;
        

        }
    }
    //public static bool _isDied = false;


}
