using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    Text textHealth;
    Player player;
    private void Start()
    {
        textHealth = GetComponent<Text>();
        player = FindObjectOfType<Player>();
    }
    private void Update()
    {
        textHealth.text = player.GetHealth().ToString();
    }
}
