using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllerScript : MonoBehaviour
{
    public Animation playButton, settingsButton, quitButton;
        [Space]
    public GameObject settingsPanel;
    public TouchControlScript tcs;

    public void QuitButton()
    {
        Application.Quit();
    }
    public void PlayButton()
    {        
        StartCoroutine(HidePlayButton());
        StartCoroutine(HideSettingsButton());
        StartCoroutine(HideQuitButton());
    }
    public void SettingsButton()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }
    IEnumerator HidePlayButton()
    {
        yield return new WaitForSeconds(0.0f);
        playButton.Play();
    }
    IEnumerator HideSettingsButton()
    {
        yield return new WaitForSeconds(0.25f);
        settingsButton.Play();
    }
    IEnumerator HideQuitButton()
    {
        yield return new WaitForSeconds(0.5f);
        quitButton.Play();
        tcs.canMove = true;///////////////////////////////////
    }
}
