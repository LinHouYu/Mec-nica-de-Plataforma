using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

namespace Mryotaisu.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [Header("Game Data")]
        public int coinsCollected = 0;
        public float totalPlayTime = 0f;
        public float currentLevelTime = 0f;
        public Vector3 respawnPosition;

        [Header("UI Elements (HUD)")]
        public TextMeshProUGUI coinText;
        public TextMeshProUGUI timerText;

        [Header("Victory Panel Elements")]
        public GameObject victoryPanel;
        public TextMeshProUGUI victoryCoinText;
        public TextMeshProUGUI victoryLevelTimeText;
        public TextMeshProUGUI victoryTotalTimeText;

        private bool _isGameActive = true;
        private GameObject _player;

        void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        void Start()
        {
            Time.timeScale = 1f;//恢复时间
            _player = GameObject.FindGameObjectWithTag("Player");
            if (_player != null)
            {
                respawnPosition = _player.transform.position;
            }
            
            victoryPanel.SetActive(false);
            UpdateHUD();
        }

        void Update()
        {
            if (_isGameActive)
            {
                totalPlayTime += Time.deltaTime;
                currentLevelTime += Time.deltaTime;
                UpdateHUD();
            }
        }

        public void AddCoin(int amount)
        {
            coinsCollected += amount;
            UpdateHUD();
        }

        public void SetCheckpoint(Vector3 newPos)
        {
            respawnPosition = newPos;
            currentLevelTime = 0f;
        }

        public void RespawnPlayer()
        {
            if (_player != null)
            {
                CharacterController cc = _player.GetComponent<CharacterController>();
                if (cc != null) cc.enabled = false;
                
                _player.transform.position = respawnPosition;
                
                if (cc != null) cc.enabled = true;
            }
        }

        public void WinGame()
        {
            _isGameActive = false;
            Time.timeScale = 0f;//暂停时间
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            victoryPanel.SetActive(true);
            victoryCoinText.text = $"Coins recolectado: {coinsCollected}/35";
            victoryLevelTimeText.text = $"Level 2: {FormatTime(currentLevelTime)}";
            victoryTotalTimeText.text = $"Total tiempo: {FormatTime(totalPlayTime)}";
        }

        public void RestartGame()
        {
            Time.timeScale = 1f;//恢复时间
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void UpdateHUD()
        {
            if (coinText != null) coinText.text = $"Coins: {coinsCollected}";
            if (timerText != null) timerText.text = FormatTime(totalPlayTime);
        }
        
        private string FormatTime(float timeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
            return string.Format("{0:D2}:{1:D2}.{2:D1}", time.Minutes, time.Seconds, time.Milliseconds / 100);
        }
    }
}