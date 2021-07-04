using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using QuantumTek.SimpleMenu;

public class DifficultySelection : MonoBehaviour
{

    public Button level1easy;

    // Start is called before the first frame update
    void Start()
    {
        level1easy.onClick.AddListener(onLevel1EasyClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onLevel1EasyClicked()
    {
        SceneManager.LoadScene("Physics Level");
        Debug.Log("Level 1 easy clicked");
    }
}
