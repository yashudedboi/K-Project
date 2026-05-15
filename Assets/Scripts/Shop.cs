using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public int Coin;
    public int Bullets;
    public TextMeshProUGUI Coin_Text;
    public TextMeshProUGUI Bullets_Text;
    public GameObject shopMenu;
    bool isShopON;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		Coin = PlayerPrefs.GetInt("Coin");
        Bullets = PlayerPrefs.GetInt("Bullets");
		//Coin = 0;
		Coin_Text.text = Coin.ToString();
		Bullets_Text.text = Bullets.ToString();
	}
    public void BuyBullets()
    {
        if (Coin > 10)
        {
            Coin -= 10;
            Coin_Text.text = Coin.ToString();

            Bullets += 10;
            Bullets_Text.text = Bullets.ToString();

            PlayerPrefs.SetInt("Coin", Coin);
            PlayerPrefs.SetInt("Bullets", Bullets);
        }
        else
        {
            print("Not enough coins");
        }
    }
    public  void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
           shopMenu.SetActive(true);
            isShopON = true;
        }
    }
    public void Resume()
    {
        shopMenu.SetActive(false);
        isShopON = false;
    }
}
