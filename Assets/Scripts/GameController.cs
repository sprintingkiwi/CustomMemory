using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject cardTemplate;
    public GameObject[] pictures;
    public Sprite[] backgrounds;
    public Card selectedCard = null;
    public bool canPlay;
    public int score;
    public Text scoreText;
    public int level;
    public List<GameObject> placedCards = new List<GameObject>();
    GameObject background;
    public AudioSource audioSource;
    public AudioClip goodSound;
    public AudioClip victorySound;

	// Use this for initialization
	void Start ()
    {
        // Load pictures prefabs
        //pictures = Resources.LoadAll("Pictures", typeof(GameObject)) as GameObject[];

        // Setup
        background = GameObject.Find("Background");
        audioSource = gameObject.GetComponent<AudioSource>();
        cardTemplate.transform.localScale = Vector3.one;
        score = 0;
        level = 1;
        PlaceCards();        
    }

    // Update is called once per frame
    void Update()
    {
        if (score >= level * 2)
        {
            audioSource.clip = victorySound;
            audioSource.Play();
            LevelUp();
        }

        //Cheats
        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("HappyBirthday");
        }
    }

    void PlaceCards()
    {
        // Empty placed cards list
        for (int i = 0; i < placedCards.Count; i++)
        {
            //placedCards[i].GetComponent<Card>().Cover();
            Destroy(placedCards[i], 1);
        }
        placedCards.Clear();

        // Choose a number of cards corresponding to the actual game level (multiplied by four, so to have 4, 8, 12, etc)
        List<GameObject> actualPictures = new List<GameObject>();
        for (int p = 0; p < (level * 2); p++)
        {
            actualPictures.Add(pictures[p]);
        }
        for (int t = 0; t < 2; t++)
        {            
            // Random assign pictures to cards
            for (int i = 0; i < actualPictures.Count; i++)
            {
                GameObject c = Instantiate(cardTemplate);
                c.GetComponent<Card>().picture = actualPictures[i];
                c.name = actualPictures[i].name;
                placedCards.Add(c);
            }
        }

        canPlay = true;
    }
	
    void ScoreAPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    void LevelUp()
    {
        if  (level >= 6)
        {
            SceneManager.LoadScene("HappyBirthday");
        }
        else
        {
            score = 0;
            scoreText.text = score.ToString();
            background.GetComponent<SpriteRenderer>().sprite = backgrounds[level];
            level += 1;
            PlaceCards();
            float newScale = (1 / (float)level + 0.2f);
            Debug.Log(newScale);
            cardTemplate.transform.localScale = new Vector3(newScale, newScale, 1f);
        }        
    }

    public IEnumerator CompareCards(Card lastCard)
    {
        canPlay = false;
        if (selectedCard != null)
        {
            if (lastCard.discovered == false)
            {
                Debug.Log("Second card selected. Comparing cards...");
                if (selectedCard.picture.name == lastCard.picture.name)
                {
                    Debug.Log("GOOD!");
                    ScoreAPoint();
                    yield return new WaitForSeconds(1f);
                    audioSource.clip = goodSound;
                    audioSource.Play();
                    yield return new WaitForSeconds(1f);
                    selectedCard = null;
                }
                else
                {
                    Debug.Log("WRONG!");                    
                    yield return new WaitForSeconds(3f);
                    selectedCard.Cover();
                    lastCard.Cover();
                    selectedCard = null;
                }
            }            
        }
        else
        {
            Debug.Log("First card selected");
            selectedCard = lastCard;
        }

        canPlay = true;
        yield return null;
    }
}
