using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance;
    private int starCount = 0;
    public int StarCount {
        get {return starCount;}
        set {starCount = value;}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null)
        {   
            Destroy(gameObject);
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {  
        
    }
}
