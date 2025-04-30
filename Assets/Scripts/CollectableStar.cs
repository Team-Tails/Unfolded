using UnityEngine;

public class CollectableStar : MonoBehaviour
{

    [SerializeField]
    Collider body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.Instance.StarCount += 1;
            Destroy(gameObject);
        }
    }

}
