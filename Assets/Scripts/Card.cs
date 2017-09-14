using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    GameController gc;
    public GameObject picture;
    public GameObject preview;
    public bool discovered;
    Sprite back;
    AudioSource audioSource;
    public AudioClip touchSound;
    public AudioClip coverSound;

    // Use this for initialization
    void Start ()
    {
        gc = GameObject.Find("Game Controller").GetComponent<GameController>();
        GoRand();
        preview = transform.Find("Preview").gameObject;
        back = preview.GetComponent<SpriteRenderer>().sprite;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
    
    public void GoRand(float hor=35, float ver=20)
    {
        transform.position = new Vector3(Random.Range(-hor, hor), Random.Range(-ver, ver), 0f);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Card")
            GoRand();
    }

    public void OnMouseDown()
    {
        if (gc.canPlay && !discovered)
        {
            audioSource.clip = touchSound;
            audioSource.Play();
            Instantiate(picture);
            Discover();
        }
    }

    void Discover()
    {
        preview.GetComponent<SpriteRenderer>().sprite = picture.GetComponent<SpriteRenderer>().sprite;
        preview.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
        StartCoroutine(gc.CompareCards(gameObject.GetComponent<Card>()));
        discovered = true;
    }

    public void Cover()
    {
        audioSource.clip = coverSound;
        audioSource.Play();
        preview.GetComponent<SpriteRenderer>().sprite = back;
        preview.transform.localScale = new Vector3(1f, 1f, 1f);
        discovered = false;
    }
}
