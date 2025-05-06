using System;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplayManager : MonoBehaviour
{

    [SerializeField]
    private Text starText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStarDisplay()
    {
        starText.text = $"{GameManager.Instance.StarCount}";
    }
}
