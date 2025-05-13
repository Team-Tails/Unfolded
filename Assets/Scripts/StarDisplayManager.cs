using System;
using UnityEngine;
using UnityEngine.UI;

public class StarDisplayManager : MonoBehaviour
{

    [SerializeField]
    private Text starText;

    public void UpdateStarDisplay()
    {
        starText.text = $"{GameManager.Instance.StarCount}";
    }
}
