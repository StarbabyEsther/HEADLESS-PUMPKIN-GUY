using UnityEngine;
using TMPro;

public class CandleScript : MonoBehaviour

{

   public AudioClip CandleSoundEffect;
   public TextMeshProUGUI CandleText;
    SoundEffectScript soundPlayer;
    PlayerScript player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CandleText = GameObject.FindWithTag("CandleText").GetComponent<TextMeshProUGUI>();
        GameObject sp_go = GameObject.Find("SoundEffectPlayer");
        soundPlayer = sp_go.GetComponent<SoundEffectScript>();
        player = GameObject.Find("player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.candle_count += 1;
            player.light_level += 1;

            soundPlayer.Play(CandleSoundEffect);

            CandleText.text = "Candles: " + player.candle_count;

            Destroy(gameObject);
        }
    }
}
