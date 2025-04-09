using UnityEngine;

public class MakeCollidedTransparent : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
  {
    print(other.name + " entered trigger,    tag: " + other.tag);
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
