using UnityEngine;

public class MakeCollidedTransparent : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("CanTransparent")) {
        other.GetComponent<MeshRenderer>().enabled = false;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("CanTransparent")) {
        other.GetComponent<MeshRenderer>().enabled = true;
    }
  }
}
