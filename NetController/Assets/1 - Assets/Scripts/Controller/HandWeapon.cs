using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Alex.Controller;
public class HandWeapon : MonoBehaviour {
    public AudioClip shootClip, reloadClip;
    private AudioSource audioManager;
    private GameObject FlasFx;

    private Animator anim;

    private string Shoot = "Shoot";
    private string Reload = "Reload";


	// Use this for initialization
	void Awake () {
        //FlasFx = transform.Find("MuzzleFlash").gameObject;
        //FlasFx.SetActive(false);
        audioManager = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
	}
	public void ShootMet()
    {
        if(audioManager.clip != shootClip)
        {
            audioManager.clip = shootClip;
        }
        audioManager.Play();
        anim.SetTrigger(Shoot);
    }
    public void reload()
    {
//        StartCoroutine(PlayReloadSound());
    }
    IEnumerator PlayReloadSound()
    {
        anim.SetTrigger(Reload);
        yield return new WaitForSeconds(0.8f);
        if(audioManager.clip != reloadClip)
        {
            audioManager.clip = reloadClip;
        }
        audioManager.Play();

    }
	// Update is called once per frame
	void Update () {
		
	}
}
