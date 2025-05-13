using UnityEngine;

public class CollectableStar : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            GameManager.Instance.StarCount += 1;
            Destroy(gameObject);
        }
    }

}
