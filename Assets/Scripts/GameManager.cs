using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    private int starCount = 0;
    public int StarCount {
        get {return starCount;}
        set {UpdateStarCount(value);}
    }
    [SerializeField]
    private StarDisplayManager starDisplay;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null)
        {   
            Destroy(gameObject);
        }
        Instance = this;
        starDisplay.UpdateStarDisplay();
    }

    private void UpdateStarCount(int value)
    {
        starCount = value;
        starDisplay.UpdateStarDisplay();
    }
}
