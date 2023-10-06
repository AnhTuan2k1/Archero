using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityMenu : MonoBehaviour
{
    [SerializeField] GameObject abilityTemplate;
    [SerializeField] Transform parentTransform;

    private void Start()
    {
        gameObject.SetActive(false);
        ShowMenu();
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);

        GameManager.Instance.TogglePause();
        ReRollAbility();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);

        GameManager.Instance.TogglePause();
        DestroyAbility();
    }

    private void DestroyAbility()
    {
        int childCount = parentTransform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parentTransform.GetChild(i);
            Destroy(child.gameObject);
            //or DestroyImmediate(child.gameObject);
        }
    }

    public void SpawnAbility()
    {
        GameObject g;
        for (int i = 0; i < 3; i++)
        {
            g = Instantiate(abilityTemplate, parentTransform);
            AbilityConfig ability = AbilityManager.Instance.getAnRandomAbilityConfig();

            g.transform.GetChild(0).GetComponent<Image>().sprite = ability.image;
            g.transform.GetChild(1).GetComponent<TMP_Text>().text = ability.title;
            g.transform.GetChild(2).GetComponent<TMP_Text>().text = ability.description;
            g.GetComponent<Button>().AddEventListener(ability.AbilityType, AbilityClicked);
        }
    }

    public void ReRollAbility()
    {
        DestroyAbility();
        SpawnAbility();
    }

    private void AbilityClicked(AbilityType type)
    {
        HideMenu();

        Player.Instance.AddAbility(type);
    }
}

public static class ButtonExtention
{
    public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate ()
        {
            OnClick(param);
        });
    }
}