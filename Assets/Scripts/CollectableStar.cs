/*
* got help from here https://www.youtube.com/watch?v=FjJJ_I9zqJo for the rotation
*/

using UnityEngine;



public class CollectableStar : MonoBehaviour
{

    void Update()
    {
        //rotate star to face camera
        transform.rotation = Quaternion.Euler(0f, Camera.main.transform.rotation.eulerAngles.y, 0f);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.StarCount += 1;
            Destroy(gameObject);
        }
    }

}
