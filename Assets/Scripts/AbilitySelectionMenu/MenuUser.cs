using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUser : MonoBehaviour {

    public string A_button;
    public string B_button;
    public string X_button;
    public string Y_button;
    public string dHorizontal;
    public string dVertical;
    public int player;
    public bool preparedToBuy;
    Ability preparedAbility;

    public float hAxis;
    public float vAxis;

    Manager manager;

    private Vector3 A = new Vector3(1.0f, 1.0f);
    private Vector3 B = new Vector3(1.0f, -1.0f);
    private Vector3 C = new Vector3(-1.0f, -1.0f);
    private Vector3 D = new Vector3(-1.0f, 1.0f);

    // Use this for initialization
    void Start() {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        preparedToBuy = false;

        A = new Vector3(1.0f, 1.0f);
        B = new Vector3(1.0f, -1.0f);
        C = new Vector3(-1.0f, -1.0f);
        D = new Vector3(-1.0f, 1.0f);

        GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Coins").GetComponent<Text>().text = manager.coinList[player - 1].ToString();

        if (manager.playerList[player - 1][0] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonDown").GetComponent<Image>().sprite = manager.playerList[player - 1][0].image;
        if (manager.playerList[player - 1][1] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonLeft").GetComponent<Image>().sprite = manager.playerList[player - 1][1].image;
        if (manager.playerList[player - 1][2] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonUp").GetComponent<Image>().sprite = manager.playerList[player - 1][2].image;
        if (manager.playerList[player - 1][3] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonRight").GetComponent<Image>().sprite = manager.playerList[player - 1][3].image;
    }

    // Update is called once per frame
    void Update()
    {
        if( GameObject.Find("CountDown").GetComponent<Timer>().getCoolTimer() > 0){ 
            if (Input.GetButton(A_button))
                prepareBuy(GameObject.Find("UI").GetComponent<AbilityMenuUI>().ability1);

            if (Input.GetButton(X_button))
                prepareBuy(GameObject.Find("UI").GetComponent<AbilityMenuUI>().ability2);

            if (Input.GetButton(Y_button))
                prepareBuy(GameObject.Find("UI").GetComponent<AbilityMenuUI>().ability3);

            if (Input.GetButton(B_button))
                prepareBuy(GameObject.Find("UI").GetComponent<AbilityMenuUI>().ability4);

            if (preparedToBuy)
            {
                hAxis = Input.GetAxis(dHorizontal);
                vAxis = Input.GetAxis(dVertical);

                if (hAxis != 0.0f || vAxis != 0.0f)
                {
                    Vector3 dir = new Vector3(hAxis, vAxis);

                    //if(Axdir * AxB >= 0 && Cxdir * CxA >=0)
                    // check if direction is between A and B (right)
                    if (Vector3.Dot(Vector3.Cross(A, dir), Vector3.Cross(A, B)) >= 0 &&
                        Vector3.Dot(Vector3.Cross(B, dir), Vector3.Cross(B, A)) >= 0)
                    {
                        checkConditions(3);
                    }
                    // check if direction is between C and D (left)
                    else if (Vector3.Dot(Vector3.Cross(C, dir), Vector3.Cross(C, D)) >= 0 &&
                            Vector3.Dot(Vector3.Cross(D, dir), Vector3.Cross(D, C)) >= 0)
                    {
                        checkConditions(1);
                    }
                    // check if direction is between D and A (up)
                    else if (Vector3.Dot(Vector3.Cross(D, dir), Vector3.Cross(D, A)) >= 0 &&
                            Vector3.Dot(Vector3.Cross(A, dir), Vector3.Cross(A, D)) >= 0)
                    {
                        checkConditions(2);
                    }
                    // check if direction is between B and C (down)
                    else if (Vector3.Dot(Vector3.Cross(B, dir), Vector3.Cross(B, C)) >= 0 &&
                            Vector3.Dot(Vector3.Cross(C, dir), Vector3.Cross(C, B)) >= 0)
                    {
                        checkConditions(0);
                    }

                }
            }
        }
    }

    void prepareBuy(Ability ability)
    {
        if (ability.price > manager.coinList[player - 1]) printMessage("You don't have enough coins to buy " + ability.title + " " + ability.level + "!");

        else
        {
            preparedToBuy = true;
            preparedAbility = ability;
            printMessage("You have selected " + ability.title + " " + ability.level);
        }
    }

    void checkConditions(int position)
    {
        bool repeated = false;
        Ability a;

        for (int i = 0; i < 4; i++){
            a = manager.playerList[player - 1][i];
            if (a.title == preparedAbility.title)
            {
                if (a.level == preparedAbility.level)
                {
                    printMessage("You already have this ability");
                    return;
                }
                if (a.level > preparedAbility.level)
                {
                    printMessage("Are you dumb? You have a best version already");
                    return;
                }
                else repeated = true;
            }
            
        }

        a = manager.playerList[player - 1][position];

        if ((preparedAbility.active && (position == 2 || position == 3)) ||
           (!preparedAbility.active && (position == 0 || position == 1)))
            {
            if ((repeated && preparedAbility.title == a.title) || !repeated)
                buyAbility(position);
            else
                printMessage("You can't two levels of the same ability");
            }
        else printMessage("You can't have an ability in the other type's slots");

    }

    void buyAbility(int position)
    {
        Ability old = manager.playerList[player - 1][position];
        printMessage("You have replaced the " + old.title + " with " + preparedAbility.title);

		if (old.title == "Empty") {
			SIS.Instance.Swapped(player, preparedAbility.price, preparedAbility.active);
		}
		else if (old.title == preparedAbility.title) {
			SIS.Instance.Upgraded(player, preparedAbility.price, preparedAbility.active);
		}
		else if (old.title != preparedAbility.title) {
			SIS.Instance.Swapped(player, preparedAbility.price, preparedAbility.active);
		}

        manager.coinList[player - 1] = manager.coinList[player - 1] - preparedAbility.price;
        manager.playerList[player - 1][position] = preparedAbility;

        preparedToBuy = false;
        preparedAbility = null;
        updateImages();
    }

    void updateImages()
    {
        if (manager.playerList[player - 1][0] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonDown").GetComponent<Image>().sprite = manager.playerList[player - 1][0].image;
        if (manager.playerList[player - 1][1] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonLeft").GetComponent<Image>().sprite = manager.playerList[player - 1][1].image;
        if (manager.playerList[player - 1][2] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonUp").GetComponent<Image>().sprite = manager.playerList[player - 1][2].image;
        if (manager.playerList[player - 1][3] != null) GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Buttons/ButtonRight").GetComponent<Image>().sprite = manager.playerList[player - 1][3].image;
        GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Coins").GetComponent<Text>().text = manager.coinList[player - 1].ToString();

    }

    void printMessage(string text)
    {
        GameObject.Find("Canvas/Panel (1)/Canvas (" + (player + 3) + ")/Panel/Text").GetComponent<Text>().text = text;
    }
}
