using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilityMenuUI : MonoBehaviour {

    public Ability ability1;
    public Sprite picture1;
    public Text price1;
    public Text title1;
    public Text description1;

    public Ability ability2;
    public Sprite picture2;
    public Text price2;
    public Text title2;
    public Text description2;

    public Ability ability3;
    public Sprite picture3;
    public Text price3;
    public Text title3;
    public Text description3;

    public Ability ability4;
    public Sprite picture4;
    public Text price4;
    public Text title4;
    public Text description4;

    Timer timer;
    public Text roundCounter;

    // Use this for initialization
    void Start () {
        timer = GameObject.Find("CountDown").GetComponent<Timer>();
        timer.startCount();

        Manager m = GameObject.Find("Manager").GetComponent<Manager>();
        roundCounter.text = m.countRounds.ToString();

        drawAbilities();
        
        price1.text = ability1.price.ToString();
        title1.text = ability1.title + " " + ability1.level;
        description1.text = ability1.description;
        GameObject.Find("Canvas/Panel/Canvas/Button").GetComponent<Image>().sprite = ability1.image;

        picture2 = ability2.image;
        price2.text = ability2.price.ToString();
        title2.text = ability2.title + " " + ability2.level;
        description2.text = ability2.description;
        GameObject.Find("Canvas/Panel/Canvas (1)/Button").GetComponent<Image>().sprite = ability2.image;

        picture3 = ability3.image;
        price3.text = ability3.price.ToString();
        title3.text = ability3.title + " " + ability3.level;
        description3.text = ability3.description;
        GameObject.Find("Canvas/Panel/Canvas (2)/Button").GetComponent<Image>().sprite = ability3.image;

        picture4 = ability4.image;
        price4.text = ability4.price.ToString();
        title4.text = ability4.title + " " + ability4.level;
        description4.text = ability4.description;
        GameObject.Find("Canvas/Panel/Canvas (3)/Button").GetComponent<Image>().sprite = ability4.image;
       
    }
	
	// Update is called once per frame
	void Update () {
        if (GameObject.Find("Canvas/Panel (2)/CountDown").GetComponent<Timer>().getCoolTimer() == 0)
        {
            SceneManager.LoadScene("Prototype");
        }
	}

    void drawAbilities()
    {
        Manager m = GameObject.Find("Manager").GetComponent<Manager>();
        List<Ability> pool = new List<Ability>(m.abilityPool);

        int index = (int)Random.Range(0, pool.Count - 1);
        ability1 = pool[index];
        pool.RemoveAt(index);

        index = (int)Random.Range(0, pool.Count - 1);
        ability2 = pool[index];
        pool.RemoveAt(index);
        while (ability2.title == ability1.title)
        {
            index = (int)Random.Range(0, pool.Count - 1);
            ability2 = pool[index];
            pool.RemoveAt(index);
        }

        index = (int)Random.Range(0, pool.Count - 1);
        ability3 = pool[index];
        pool.RemoveAt(index);
        while (ability3.title == ability1.title || ability3.title == ability2.title)
        {
            index = (int)Random.Range(0, pool.Count - 1);
            ability3 = pool[index];
            pool.RemoveAt(index);
        }

        index = (int)Random.Range(0, pool.Count - 1);
        ability4 = pool[index];
        pool.RemoveAt(index);
        while (ability4.title == ability1.title || ability4.title == ability2.title || ability4.title == ability3.title)
        {
            index = (int)Random.Range(0, pool.Count - 1);
            ability4 = pool[index];
            pool.RemoveAt(index);
        }
    }
}
