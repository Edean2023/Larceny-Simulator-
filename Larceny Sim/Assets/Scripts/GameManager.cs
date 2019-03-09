using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // This is the singleton 
    public static GameManager instance;
    // recognizes the player object
    public PlayerMovement player;
    // Awake runs before anything else
    private void Awake()
    {
        // This sets up the singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
