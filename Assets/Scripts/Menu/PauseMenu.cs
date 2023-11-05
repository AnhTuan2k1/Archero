
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject girdLayoutSkills;
    [SerializeField] GameObject contentPrefab;
    [SerializeField] List<Image> acquiredSkills;
    [SerializeField] bool isSkillsShowed;

    [SerializeField] GameObject attibutes;
    [SerializeField] TextMeshProUGUI textAcquiredSkills;
    [SerializeField] TextMeshProUGUI textPlayerAttributes;
    [SerializeField] TextMeshProUGUI textMaxHp;
    [SerializeField] TextMeshProUGUI textDamage;
    [SerializeField] TextMeshProUGUI textAttackSpeed;
    [SerializeField] TextMeshProUGUI textCritRate;
    [SerializeField] TextMeshProUGUI textBloodThirstRate;
    [SerializeField] TextMeshProUGUI textRageRate;
    [SerializeField] TextMeshProUGUI textPoisonedRate;
    [SerializeField] TextMeshProUGUI textBlazeRate;
    [SerializeField] TextMeshProUGUI textBoltRate;


    private void Start()
    {
        gameObject.SetActive(false);
        acquiredSkills = new List<Image>();
    }

    public void ShowMenu()
    {
        gameObject.SetActive(true);
        GameManager.Instance.OnGamePaused();
        ShowSkills();
        ShowPlayerAttributes();
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        GameManager.Instance.OnGameResume();
        if (!isSkillsShowed) ButtonSwitch();
    }

    public void ButtonSwitch()
    {
        isSkillsShowed = !isSkillsShowed;
        float time = 0.2f;
        if (isSkillsShowed)
        {
            textPlayerAttributes.transform.DORotate(new Vector3(0, 90, 0), time)
                .SetEase(Ease.Linear);
            attibutes.transform.DORotate(new Vector3(0, 90, 0), time)
                .SetEase(Ease.Linear);
            girdLayoutSkills.transform.DORotate(new Vector3(0, 0, 0), time)
                .SetEase(Ease.Linear)
                .SetDelay(time);
            textAcquiredSkills.transform.DORotate(new Vector3(0, 0, 0), time)
                .SetEase(Ease.Linear)
                .SetDelay(time);

            //ShowSkills();
        }

        else
        {
            textPlayerAttributes.transform.DORotate(new Vector3(0, 0, 0), time)
                .SetEase(Ease.Linear)
                .SetDelay(time);
            attibutes.transform.DORotate(new Vector3(0, 0, 0), time)
                .SetEase(Ease.Linear)
                .SetDelay(time);
            girdLayoutSkills.transform.DORotate(new Vector3(0, -90, 0), time)
                .SetEase(Ease.Linear);
            textAcquiredSkills.transform.DORotate(new Vector3(0, -90, 0), time)
                .SetEase(Ease.Linear);
        }
    }

    private void ShowPlayerAttributes()
    {
        Player player = Player.Instance;
        textMaxHp.text = ((int)(player.Maxhp)).ToString();
        textDamage.text = ((int)(player.Damage)).ToString();
        textAttackSpeed.text = (1/player.playerAttack.AttackSpeed).ToString();
        textCritRate.text = (((int)(player.CritRate*10000))/100).ToString() + "%";

        textBloodThirstRate.text = ((int)(player.BloodThirstRate * 100)).ToString()
            + "%MaxHp = " + (int)(player.Maxhp * player.BloodThirstRate);

        textRageRate.text = ((int)(player.RageRate * 100)).ToString()
            + "%Dmg = " + (int)(player.RageRate * player.Damage / (player.RageRate + 1));

        textPoisonedRate.text = ((int)(player.PoisonedRate * 100)).ToString()
            + "%Dmg = " + (int)(player.Damage * player.PoisonedRate);

        textBlazeRate.text = ((int)(player.BlazeRate * 100)).ToString()
            + "%Dmg = " + (int)(player.Damage * player.BlazeRate);

        textBoltRate.text = ((int)(player.BoltRate * 100)).ToString() 
            + "%Dmg = " + (int)(player.Damage * player.BoltRate);
    }

    private void ShowSkills()
    {
        int i = 0;
        int num = acquiredSkills.Count;

        Player.Instance.Abilities.ForEach((ability) =>
        {
            if(i < num)
            {
                acquiredSkills[i].sprite = 
                AbilityManager.Instance.getSpriteAbilityConfig(ability);
            }
            else
            {
                GameObject content = Instantiate(contentPrefab);
                content.GetComponent<Image>().sprite =
                AbilityManager.Instance.getSpriteAbilityConfig(ability);
               
                acquiredSkills.Add(content.GetComponent<Image>());
                content.transform.SetParent(girdLayoutSkills.transform);
            }

            acquiredSkills[i].transform.localScale = Vector3.zero;
            acquiredSkills[i].transform.DOScale(1, 0.5f)
            .SetDelay(0.05f * i).SetEase(Ease.OutSine);

            acquiredSkills[i].gameObject.SetActive(true);
            i++;
        });

        if (acquiredSkills.Count > 16)
        {
            if (girdLayoutSkills.TryGetComponent<RectTransform>(out var rectTransform))
            {
                float height = 135 * (num / 4 + 1) + 25 * (num / 4);
                Rect rect = rectTransform.rect;
                //rectTransform.rect.Set(rect.x, rect.y, rect.width, height);
                girdLayoutSkills.GetComponent<RectTransform>()
                    .rect.Set(rect.x, rect.y, rect.width, height);
            }
        }
    }
}