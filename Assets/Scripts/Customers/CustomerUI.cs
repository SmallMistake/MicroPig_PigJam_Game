using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class CustomerUI : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Animator customerAnimator;

    [SerializeField]
    private ParticleSystem happyParticleSystem;

    [SerializeField]
    private ParticleSystem angryParticleSystem;

    public void SetCustomer(Customer customer)
    {
        if (customer != null)
        {
            spriteRenderer.sprite = customer.sprites[Random.Range(0, customer.sprites.Count)];
            customerAnimator.SetTrigger("IntroduceCustomer");
            //customerAnimator.SetTrigger("FinishCustomer");

        }
        else
        {
            happyParticleSystem.Play();
        }
    }


    public void ExitStore()
    {
        customerAnimator.SetTrigger("FinishCustomer");
    }
}
