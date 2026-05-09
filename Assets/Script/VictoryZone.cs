using UnityEngine;

namespace Mryotaisu.Scripts
{
    public class VictoryZone : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.WinGame();
            }
        }
    }
}