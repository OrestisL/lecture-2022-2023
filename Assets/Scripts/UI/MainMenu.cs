using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Utilities;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    public Button highscores;
    public Button startGame;
    public Button settings;
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject highscoresPanel;
    [SerializeField] public RectTransform scores;
    [Header("Sliders")]
    public Slider bgSlider;
    public Slider sfxSlider;
    [Header("UI Prefabs")]
    public GameObject highscoreEntryPrefab;

   

    private void Start()
    {
        settings.onClick.AddListener(() => settingsPanel.SetActive(!settingsPanel.activeSelf));
        highscores.onClick.AddListener(() => ShowHighscores());
        startGame.onClick.AddListener(() => StartCoroutine(Util.LoadSceneAsync(2, startGame)));

        bgSlider.value = Audio.Instance.bgMusicSource.volume;
        sfxSlider.value = Audio.Instance.sfxSource.volume;

        bgSlider.onValueChanged.AddListener(Audio.Instance.SetBGAudioVolume);
        sfxSlider.onValueChanged.AddListener(Audio.Instance.SetSFXAudioVolume);
    }

    void ShowHighscores() 
    {

        if (highscoresPanel.activeInHierarchy)
        {
            highscoresPanel.SetActive(false);
            for (int i = 0; i < highscoresPanel.transform.GetChild(2).childCount; i++)
            {
                Destroy(highscoresPanel.transform.GetChild(2).GetChild(i).gameObject);
            }
            return;
        }

        List<Highscore> list = Util.LoadHighscores();

        if (list == null)
        {
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = Instantiate(highscoreEntryPrefab, highscoresPanel.transform.GetChild(2));
            temp.GetComponent<HighscoreEntry>().SetValues<int>(list[i].name, list[i].score);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(scores);
        highscoresPanel.SetActive(true);
    }
}
