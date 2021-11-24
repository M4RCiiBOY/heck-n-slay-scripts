using System;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    public float maxHealth;

    public float attackRange;
    public float chaseRange;
    public float stopDistance;

    public Image HealthBar;
    public GameObject HealthBarObject;

    private bool healtBarEnabled;
    public Canvas canvas;
	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
        EnableDisableHealthBar(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (healtBarEnabled)
        {
            canvas.transform.position = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);
        }
	}


    public void EnableDisableHealthBar(bool enable)
    {
        HealthBarObject.SetActive(enable);
        healtBarEnabled = enable;
    }

    public void DrawHealthBar()
    {
        HealthBar.fillAmount = (currentHealth / maxHealth);
    }

    public void GetDamage(float dmg)
    {
        currentHealth -= dmg;
        DrawHealthBar();
    }
}
