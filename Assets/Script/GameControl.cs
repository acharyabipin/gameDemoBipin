using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    public static event Action HandlePulled = delegate{ };

    public sfx_btn btn_sound;

    public sfx_handle handleSound;
    
    [SerializeField]
    private Text prizeText;

    [SerializeField]
    private Text totalPrizeText;
   
    [SerializeField]
    private Text betText;
   
    [SerializeField]
    private Slider betSlider;

    [SerializeField]
    private Text creditText;
    
    [SerializeField]
    private Row[] rows;

    [SerializeField]
    private Transform handle;

    private int prizeValue;
    private int totalPrize = 0;
    private int credit = 1000;

    private  float betRate;

    private int betRate_int = 1;




    private bool resultsChecked = false;


    // Start is called before the first frame update
    void Start()
    {
        creditText.text="Credit: "+ credit;
        totalPrizeText.text="Total Won Prize";
    }

    // Update is called once per frame
    void Update()
    {
        if(!rows[0].rowStopped ||!rows[1].rowStopped || !rows[2].rowStopped){
            prizeValue =0;
            prizeText.enabled = false;
            resultsChecked = false;
        }
        
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && !resultsChecked){
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "Prize: "+ prizeValue;
        }

    }

    private void OnMouseDown()
    {
        if(rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped){

            if(credit >=100 * betRate_int){
                handleSound.playHandleSound();
                StartCoroutine("PullHandle");
                credit = credit-100* betRate_int;
                creditText.text="Credit: "+ credit; 
            }else{
                creditText.text="Credit not sufficient";
            }
        }
    }

    private IEnumerator PullHandle()
    {
        for(int i=0; i<15; i+=5){
            handle.Rotate(0f,0f,i);
            yield return new WaitForSeconds(0.1f);
        }
        
        HandlePulled();

        for(int i=0; i<15; i+=5){
            handle.Rotate(0f,0f,-i);
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    private void CheckResults()
    {
       if(rows[0].stoppedSlot == "Diamond" && rows[1].stoppedSlot == "Diamond" && rows[2].stoppedSlot == "Diamond")
       {
        prizeValue = 200;
       }
       else if(rows[0].stoppedSlot == "Crown" && rows[1].stoppedSlot == "Crown" && rows[2].stoppedSlot == "Crown")
       {
        prizeValue = 400;
       }
       else if(rows[0].stoppedSlot == "Melon" && rows[1].stoppedSlot == "Melon" && rows[2].stoppedSlot == "Melon")
       {
        prizeValue = 600;
       }
       else if(rows[0].stoppedSlot == "Bar" && rows[1].stoppedSlot == "Bar" && rows[2].stoppedSlot == "Bar")
       {
        prizeValue = 800;
       }
       else if(rows[0].stoppedSlot == "Seven" && rows[1].stoppedSlot == "Seven" && rows[2].stoppedSlot == "Seven")
       {
        prizeValue = 1500;
       }
       else if(rows[0].stoppedSlot == "Cherry" && rows[1].stoppedSlot == "Cherry" && rows[2].stoppedSlot == "Cherry")
       {
        prizeValue = 3000;
       }
       else if(rows[0].stoppedSlot == "Lemon" && rows[1].stoppedSlot == "Lemon" && rows[2].stoppedSlot == "Lemon")
       {
        prizeValue = 5000;
       }
       else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Diamond"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Diamond"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Diamond"))){
            prizeValue = 100;
        }
        else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Crown"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Crown"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Crown"))){
            prizeValue = 300;
        }
        else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Melon"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Melon"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Melon"))){
            prizeValue = 500;
        }
        else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Bar"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Bar"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Bar"))){
            prizeValue = 700;
        }
        else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Seven"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Seven"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Seven"))){
            prizeValue = 1000;
        }
        else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Cherry"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Cherry"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Cherry"))){
            prizeValue = 2000;
        }
        else if(
        ((rows[0].stoppedSlot == rows[1].stoppedSlot) && (rows[0].stoppedSlot == "Lemon"))
        || ((rows[0].stoppedSlot == rows[2].stoppedSlot) && (rows[0].stoppedSlot == "Lemon"))
        || ((rows[1].stoppedSlot == rows[2].stoppedSlot) && (rows[1].stoppedSlot == "Lemon"))){
            prizeValue = 4000;
        }
        resultsChecked = true;
        if(prizeValue >=100)
        {
            handleSound.playWonPrize();
        }
        prizeValue = prizeValue * betRate_int;
        totalPrize = totalPrize + prizeValue;
        totalPrizeText.text="Total Won Prize : "+ totalPrize;
    }


    public void addCreditFromPrize(){
        credit = credit + totalPrize;
        totalPrize = 0;

        totalPrizeText.text="Total Won Prize : "+ totalPrize;
         creditText.text="Credit: "+ credit; 
        print("Total Credit" + credit);
    }


    public void changeBetRate()
    {
        btn_sound.btn_pressSound();
        betRate = betSlider.value;
        betRate = betRate *100;
        betRate_int = (int) betRate;
        if(betRate_int<=0){
        betRate_int = 1;   
        }

        string betRate_str = betRate_int.ToString();
        
        betText.text =  betRate_str + "X";
        
    }

}
