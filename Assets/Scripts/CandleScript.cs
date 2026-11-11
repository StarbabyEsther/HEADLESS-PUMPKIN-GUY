using UnityEngine;

public class CandleScript : MonoBehaviour
{
    public AudioClip CandleSoundEffect;

    private void Start()
    {
        Debug.LogWarning("CandleScript is running!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("Candle touched by " + collision.name);

        if (collision.CompareTag("Player"))
        {
            Debug.LogWarning("Player collected candle!");

            if (CandleSoundEffect != null)
            {
                AudioSource.PlayClipAtPoint(
                    CandleSoundEffect,
                    Camera.main != null ? Camera.main.transform.position : transform.position,
                    1f
                );

                Debug.LogWarning("Pickup sound triggered!");
            }
            else
            {
                Debug.LogError("CandleSoundEffect is NOT assigned!");
            }

            Destroy(gameObject);
        }
    }
}