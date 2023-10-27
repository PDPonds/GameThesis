using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_animmationController : MonoBehaviour
{
    [SerializeField] private Animator cashAnim;
    [SerializeField] private Animator addCashAnim;
    [SerializeField] private Animator removeCashAnim;

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

    private void AddCashAnim()
    {
        addCashAnim.Play("Add Cash");
        cashAnim.Play("Deduct Cash Blink");
    }

    private void RemoveCashAnim()
    {
        removeCashAnim.Play("Deduct Cash");
        cashAnim.Play("Add Cash Blink");
    }
}
