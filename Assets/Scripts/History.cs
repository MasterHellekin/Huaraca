using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    [SerializeField] TextAsset txtFile;
    [SerializeField] Sprite sprite;
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] GameObject storyImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadText()
    {
        textDisplay.text = txtFile.text.ToString();
        storyImage.GetComponent<Image>().sprite = sprite;
    }
}
