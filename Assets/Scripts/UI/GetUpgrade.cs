using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUpgrade : MonoBehaviour {
    public List<Upgrade> upgradeList = new List<Upgrade>();
    public string unlock_jingle;
    private AudioSource unlock_jingle_sound;
   

    public GameObject upgrade_display_UI;
    public Image upgrade_image;
    public Text title;
    public Text description;

    public bool on_display = false;
    public float min_time_display;
    private bool can_close;

	// Use this for initialization
	void Start () {
        unlock_jingle_sound = FindObjectOfType<SFXManager>().FindSFX(unlock_jingle);
    }
	
	// Update is called once per frame
	void Update () {
        upgrade_display_UI.gameObject.SetActive(on_display);

        if (on_display && can_close)
        {
            if (Input.GetButtonDown("Submit") && !unlock_jingle_sound.isPlaying)
            {
                CloseUpgradeScreen();
                FindObjectOfType<MusicController>().ResetVolume();
                can_close = false;
            }
        }
    }

    public void DisplayUpgradeScreen(string name)
    {

        unlock_jingle_sound.Play();
        FindObjectOfType<MusicController>().ChangeVolume(0);
        on_display = true;
        Time.timeScale = 0f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>().SwitchInput(false);
        Upgrade upgrade = FindUpgrade(name);
        upgrade_image.sprite = upgrade.upgrade_image;
        title.text = upgrade.title;
        description.text = upgrade.description;
        upgrade_image.preserveAspect = true;

        can_close = true;
        StartCoroutine(UpgradeDisplayTimer());
    }
    public void CloseUpgradeScreen()
    {
        on_display = false;
        Time.timeScale = 1f;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInputManager>().SwitchInput(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity = new Vector2(0, GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().velocity.y);
        upgrade_image.sprite = null;
        title.text = null;
        description.text = null;
    }

    public Upgrade FindUpgrade(string name)
    {
        Upgrade return_val = null;
        for (int i = 0; i < upgradeList.Count; i++)
        {
            if (name == upgradeList[i].name)
            {
                return_val = upgradeList[i];
                break;
            }
        }
        return return_val;
    }
    
    IEnumerator UpgradeDisplayTimer()
    {
        yield return new WaitForSeconds(min_time_display);
        can_close = true;
    }
}

[System.Serializable]
public class Upgrade
{
    public string name;
    public Sprite upgrade_image;
    public string title;
    public string description;
}
