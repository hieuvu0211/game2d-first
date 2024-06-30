using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI myText;

    void Start()
    {
        Debug.Log("your Point = " + PlayerPrefs.GetInt("YourPoint"));
        myText.text = PlayerPrefs.GetInt("YourPoint").ToString();
    }
    public void YourScore()
    {
        myText.text = PlayerPrefs.GetInt("YourPoint").ToString();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
