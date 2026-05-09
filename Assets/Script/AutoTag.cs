using UnityEngine;

public class AutoTag : MonoBehaviour
{
    void Awake()
    {
  
        foreach (Transform child in GetComponentsInChildren<Transform>())
        {
            child.gameObject.tag = "Ground";
        }
    }
}