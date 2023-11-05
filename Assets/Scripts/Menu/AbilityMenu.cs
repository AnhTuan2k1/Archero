using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenu : MonoBehaviour
{
    [SerializeField] GameObject[] abilityTemplate;
    [SerializeField] Transform parentTransform; 
    [SerializeField] GameObject blockPanel;
    [SerializeField] GameObject reroll;

    private void Start()
    {
        gameObject.SetActive(false);

        Invoke(nameof(ShowMenu), 1);
    }

    public void ShowMenu()
    {
        reroll.SetActive(true);
        gameObject.SetActive(true);

        GameManager.Instance.OnGamePaused();
        ReRollAbility();
        reroll.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameResume();
    }

    public void SpawnAbility()
    {
        ActiveBlockPanel();

        for (int i = 0; i < abilityTemplate.Length; i++)
        {
            StartCoroutine(SpinAbility(abilityTemplate[i], 500 * (i + 2)));
        }

        Invoke(nameof(UnActiveBlockPanel), 0.5f * (abilityTemplate.Length + 1));
    }

    private void ActiveBlockPanel() => blockPanel.SetActive(true);
    private void UnActiveBlockPanel() => blockPanel.SetActive(false);

    public void ReRollAbility()
    {
        if (reroll)
        {
            SpawnAbility();
            reroll.SetActive(false);
        }
    }

    private void AbilityClicked(AbilityType type)
    {
        HideMenu();

        Player.Instance.AddAbility(type);
    }

    private IEnumerator SpinAbility(GameObject g, int milliseconds)
    {
        AbilityManager abilityManager = AbilityManager.Instance;
        Image image = g.transform.GetChild(0).GetComponent<Image>();
        TMP_Text title = g.transform.GetChild(1).GetComponent<TMP_Text>();
        TMP_Text description = g.transform.GetChild(2).GetComponent<TMP_Text>();

        AbilityConfig ability = abilityManager.getAnRandomAbilityConfig();
        while (milliseconds > 0)
        {
            if (milliseconds % 50 == 0)
                AudioManager.instance.PlaySound(Sound.Name.SkillRotating);
            ability = abilityManager.getAnRandomAbilityConfig();

            image.sprite = ability.image;
            title.text = ability.title;
            description.text = ability.description.Replace('#', '\n');

            yield return new WaitForSeconds(0.025f);
            milliseconds -= 25;
        }


        g.GetComponent<Button>().AddEventListener(ability.AbilityType, AbilityClicked);

        AudioManager.instance.PlaySound(Sound.Name.SkillRotateEnd);
    }
}

public static class ButtonExtention
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}