using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private float currentMana;

    [SerializeField]
    private float currrentXP;
    [SerializeField]
    private float currentLevel;

    public float maxHealth;
    public float maxMana;
    public float maxXP;
    public float maxLevel;

    public Image healthBar;
    public Image manaBar;
    public Image xPBar;
    public Text levelText;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        currrentXP = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= 10;
            currentMana -= 50;
            currrentXP += 10;
            DrawPlayerBar(healthBar, currentHealth, maxHealth);
            DrawPlayerBar(manaBar, currentMana, maxMana);
            DrawPlayerBar(xPBar, currrentXP, maxXP);
        }
    }

    public void DrawPlayerBar(Image bar,float currentValue,float maxValue)
    {
        Debug.Log(currentValue / maxValue);
        bar.fillAmount = (currentValue / maxValue);
    }
}
