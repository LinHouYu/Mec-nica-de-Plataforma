using UnityEngine;

namespace Mryotaisu.Scripts
{
    public class Coin : MonoBehaviour
    {
        public int coinValue = 1;
        public float rotationSpeed = 100f;
        public AudioClip CoinSound;

        void Update()
        {
            // 让金币自己旋转，看起来更好看
            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.AddCoin(coinValue);
                AudioSource.PlayClipAtPoint(CoinSound, transform.position);
                Destroy(gameObject); 
            }
        }
        
        
    }
}