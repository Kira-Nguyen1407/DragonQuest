using UnityEngine;
using UnityEngine.UI;
using System;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; // Sound: or Music:

    [SerializeField] private Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateVolume(){
        float volumeValue = (float)Math.Round(PlayerPrefs.GetFloat(volumeName)*100, 0);
        _text.text = textIntro + volumeValue.ToString();
    }

    public void DisplayVolume(){
        float volumeValue = (float)Math.Round(PlayerPrefs.GetFloat(volumeName), 1);
        Debug.Log("Volume before rounding: " + PlayerPrefs.GetFloat(volumeName));
        Debug.Log("Volume value: " + volumeValue);
        _text.text = volumeValue.ToString();
    }
}
