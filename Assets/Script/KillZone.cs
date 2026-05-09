using UnityEngine;

namespace Mryotaisu.Scripts
{
    public class KillZone : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.RespawnPlayer();
            }
        }
    }
}