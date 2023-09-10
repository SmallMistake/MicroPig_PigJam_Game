using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class CustomerUI : MonoBehaviour
{
    [SerializeField]
    private Image image;

    [SerializeField]
    private TextMeshProUGUI nameLabel, typeLabel;

    [SerializeField]
    private CanvasGroup canvasGroup;

    private void OnDisable()
    {
        this.transform.DOKill();
    }

    public void SetCustomer(Customer customer)
    {
        if (customer != null)
        {
            this.canvasGroup.alpha = 0f;
            this.canvasGroup.DOFade(1f, 0.3f);
            this.image.sprite = customer.sprites[Random.Range(0, customer.sprites.Count)];
            this.nameLabel.text = customer.customerName;
            this.typeLabel.text = customer.gameType;
            this.transform.DOKill();
            this.transform.localScale = Vector3.one * 0.75f;
            this.transform.DOScale(1f,2f);

        }
        else
        {
            this.canvasGroup.DOKill();
            this.canvasGroup.alpha = 0f;
            this.transform.DOKill();
            this.nameLabel.text = "";
            this.typeLabel.text = "";
        }
    }

   

}
