using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class CoroutineExamples : MonoBehaviour
{
    [SerializeField] private float _timer;
    [SerializeField] private int objectsAmount;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Image img;
    private void Start()
    {
        StartCoroutine(Example());
        //StopCoroutine(Example());
    }

    IEnumerator Example()
    {
        float timer = 0;

        while (timer < _timer)
        {
            timer += Time.deltaTime;
            img.fillAmount = timer / _timer;
            yield return new WaitForEndOfFrame();
        }

        GameObject parent = new GameObject();
        GameObject[] prefabs = new GameObject[objectsAmount];
        parent.SetActive(false);
        for (int i = 0; i < objectsAmount; i++)
        {
            prefabs[i] = Instantiate(prefab, Random.onUnitSphere, Quaternion.identity, parent.transform);
            yield return new WaitForSeconds(0.2f);
        }

        for (int i = 0; i < objectsAmount; i++)
        {
            prefabs[i].transform.parent = null;
        }

        Destroy(parent);
    }
}
