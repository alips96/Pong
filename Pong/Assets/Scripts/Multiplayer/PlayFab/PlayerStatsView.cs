using UnityEngine;
using UnityEngine.UI;

namespace Pong.MP.PlayFab
{
    public class PlayerStatsView : MonoBehaviour
    {
        [SerializeField] private float alphaComponent = 0.3f;

        public Text totalGamesText;
        public Text wonText;
        public Text lossText;
        public Text percentageText;
        [SerializeField] private Transform statsPanel;

        internal void DisplayPanel()
        {
            transform.parent.GetComponent<Image>().color = new Color(0, 0, 0, alphaComponent);
            statsPanel.gameObject.SetActive(true);
        }

        public void SwitchOffPanel()
        {
            transform.parent.GetComponent<Image>().color = Color.clear;
            statsPanel.gameObject.SetActive(false);
        }
    }
}