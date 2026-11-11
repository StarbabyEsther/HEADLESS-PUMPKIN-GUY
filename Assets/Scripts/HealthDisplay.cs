using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite EmptyCandle;
    public Sprite OpenCandle;
    public Image[] Candles;
    public PlayerScript playerScript;
   
    void Update()
    {

        for (int i = 0; i < Candles.Length; i++)
        {
            if (i < maxHealth)
            {
                Candles[i].enabled = true;
            }
            else
            {
                Candles[i].enabled = false;
            }

            if (i < playerScript.health)
            {
                Candles[i].sprite = OpenCandle;
            }
            else
            {
                Candles[i].sprite = EmptyCandle;
            }
        }
    }
}