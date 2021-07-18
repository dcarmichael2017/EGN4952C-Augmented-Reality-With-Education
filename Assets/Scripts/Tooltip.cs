using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{

    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;

    public void SetText(string content, string header = "")
    {
        /*if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else{
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        if ((headerLength > characterWrapLimit) || (contentLength > characterWrapLimit)){
            layoutElement.enabled = true;
        }
        else {
            layoutElement.enabled = false;
        }*/
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        if ((headerLength > characterWrapLimit) || (contentLength > characterWrapLimit)){
            layoutElement.enabled = true;
        }
        else {
            layoutElement.enabled = false;
        }
    }

}
