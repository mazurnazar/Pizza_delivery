using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AddPoints : MonoBehaviour
{
    [SerializeField] Text totalMoneyText;
    [SerializeField] public Customer customer;
    int totalMoney;
    // shoe money text
    public void ShowMoney(int money)
    {
        totalMoney += money;
        totalMoneyText.text = totalMoney + "$";
        totalMoneyText.gameObject.SetActive(true);
    }
    
}
