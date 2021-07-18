using TMPro;
using UnityEngine;
using UnityEngine.UI;

    public class DisplayStats : MonoBehaviour
    {
        [SerializeField] private TMP_Text totalEnemiesText;
        [SerializeField] private TMP_Text deployedEnemiesText;
        [SerializeField] private TMP_Text AliveEnemiesText;

        public int totalEnemiesAmount;
        public int deployedEnemiesAmount;
        public int aliveEnemiesAmount;

        public void SetTotalEnemies(int amount)
        {
            totalEnemiesAmount += amount;
            totalEnemiesText.text = "Total Enemies: " + totalEnemiesAmount;
        }

        public void SetDeployedEnemies(int amount)
        {
            deployedEnemiesAmount += amount;
            deployedEnemiesText.text = "Deployed Enemies: " + deployedEnemiesAmount;
        }

        public void SetAliveEnemies(int amount)
        {
            aliveEnemiesAmount = amount;
            AliveEnemiesText.text = "Alive Enemies: " + aliveEnemiesAmount;
        }
    }