using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public Upgrade upgrade;
    public TMP_Text title;
    public TMP_Text description;
    public Image image;

    public void SetUpgrade(Upgrade upgrade)
    {
		this.upgrade = upgrade;
        title.text = upgrade.title;
        description.text = upgrade.description;
        image.sprite = upgrade.image;
	}

    public void SelectUpgrade()
    {
		GameManager.instance.SetUpgrade(upgrade);
	}
}
