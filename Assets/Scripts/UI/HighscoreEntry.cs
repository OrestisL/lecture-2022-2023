using UnityEngine;
using TMPro;

public class HighscoreEntry : MonoBehaviour
{
    public TextMeshProUGUI hsName, score;

    public void SetValues<T>(string name, T amount)
    {
        hsName.text = name;
        score.text = amount.ToString();
    }
}
