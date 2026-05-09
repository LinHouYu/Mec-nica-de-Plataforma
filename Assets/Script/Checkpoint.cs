using UnityEngine;

namespace Mryotaisu.Scripts
{
    public class Checkpoint : MonoBehaviour
    {
        private bool _isActivated = false;

        void OnTriggerEnter(Collider other)
        {
 
            if (other.CompareTag("Player") && !_isActivated)
            {
                _isActivated = true;
   
                Vector3 safePos = transform.position + new Vector3(0, 1f, 0);
                GameManager.Instance.SetCheckpoint(safePos);
                
            
                Debug.Log("存档成功！");
            }
        }
    }
}