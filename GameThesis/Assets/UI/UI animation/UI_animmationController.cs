using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_animmationController : MonoBehaviour
{
    [SerializeField] private Animator cashAnim;
    [SerializeField] private Animator addCashAnim;
    [SerializeField] private TextMeshProUGUI amountText;

    [SerializeField] private Animator ratingTextAnim;
    [SerializeField] private Animator ratingStarAnim;

    void Start()
    {
        cashAnim = GetComponent<Animator>();
    }

    public void AddCashAnim(float amount)
    {
        amountText.text = $"+$ {amount.ToString("F2")}";
        addCashAnim.Play("Add Cash");
        cashAnim.Play("Add Cash Blink");        
    }

    public void RemoveCashAnim(float amount)
    {
        amountText.text = $"-$ {amount.ToString("F2")}";
        addCashAnim.Play("Add Cash");
        cashAnim.Play("Deduct Cash Blink");
    }

    public void AddRatingAnim()
    {
        ratingTextAnim.Play("Add Rating");
        ratingStarAnim.Play("Add Rating Star");
    }

    public void RemoveRatingAnim()
    {
        ratingTextAnim.Play("Remove Rating");
        ratingStarAnim.Play("Remove Rating Star");
    }
}
