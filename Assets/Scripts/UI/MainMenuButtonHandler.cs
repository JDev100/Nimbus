using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonHandler : MonoBehaviour {
    public string menu_scroll_sound;
    private AudioSource menu_scroll;


    public Button[] button_list;
    private int index;
    // Use this for initialization
    void Start () {
        menu_scroll = FindObjectOfType<SFXManager>().FindSFX(menu_scroll_sound);

        SelectFirst();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (index == 0)
                index = button_list.Length - 1;
            else
                index--;

            SelectNext(index);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (index == button_list.Length - 1)
                index = 0;
            else
                index++;

            SelectNext(index);
        }
    }
    private void SelectFirst()
    {
        button_list[0].Select();
        index = 0;

    }
    private void SelectNext(int i)
    {
        button_list[i].Select();
        MenuScroll();
    }

    public void MenuScroll()
    {
        menu_scroll.Play();
    }
}
