using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_animmationController : MonoBehaviour
{
    [SerializeField] private Animator cashAnim;
    [SerializeField] private Animator addCashAnim;
    [SerializeField] private Animator removeCashAnim;

    [SerializeField] private Animator ratingTextAnim;
    [SerializeField] private Animator ratingStarAnim;

    void Start()
    {
        cashAnim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            AddCashAnim();
            RemoveCashAnim();
        }
    }

    public void AddCashAnim()
    {
        addCashAnim.Play("Add Cash");
        cashAnim.Play("Add Cash Blink");        
    }

    public void RemoveCashAnim()
    {
        removeCashAnim.Play("Deduct Cash");
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
