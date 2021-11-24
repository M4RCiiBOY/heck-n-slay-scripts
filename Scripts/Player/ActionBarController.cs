using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(PlayerController))]
public class ActionBarController : MonoBehaviour
{
    public List<Spell> spells = new List<Spell>();
    public Button[] buttons;
    private PlayerController player;
    public Text helpingText;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void ActionBarButton(int button)
    {
        if (player.enemy == null)
        {
            helpingText.text = "No Target";
            StartCoroutine(NoTargetText());
            return;
        }
        float dist = Vector3.Distance(player.transform.position, player.enemy.transform.position);
        if (dist > spells[button].spellRange)
        {
            helpingText.text = "Out of Range";
            StartCoroutine(NoTargetText());

        }
        else
        {
            StartCoroutine(CastSpell(spells[button]));
        }
    }

    private IEnumerator NoTargetText()
    {
        helpingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        helpingText.gameObject.SetActive(false);

    }

    // TODO: Attack anim + (SpellParticls)
    private IEnumerator CastSpell(Spell spell)
    {



        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        yield return new WaitForSeconds(spell.castTime);

        player.enemy.GetComponent<EnemyStats>().GetDamage(spell.spellDmg);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }



    }

}
