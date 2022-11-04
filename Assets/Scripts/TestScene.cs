using UnityEngine;
using UnityEngine.UI;
using Utilities;

public class TestScene : MonoBehaviour
{
    public Button increaseScore, returnToMainMenu;
    public string playerName;
    public int score;

    private void Start()
    {
        if (playerName == "")
        {
            playerName = System.DateTime.Now.ToString("dddd") + Random.Range(0, 10);
        }

        increaseScore.onClick.AddListener(() => score++);
        returnToMainMenu.onClick.AddListener(() => 
        {
            Util.SaveScore(playerName, score);
            StartCoroutine(Util.LoadSceneAsync("Main Menu", returnToMainMenu)); 
        });
    }

}
