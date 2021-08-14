using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [Header("Skull")]
    [SerializeField] GameObject Skull1;
    [SerializeField] GameObject Skull2;
    [SerializeField] csPlayer csPlayer1;
    [SerializeField] csPlayer csPlayer2;

    csPlayer currentPlayer;
    bool currentstats = false;
    public bool Currentstats
    {
        get { return currentstats; }
    }

    float changeCoolTime = 5.0f;
    float changeTime = 0f;

    void Awake()
    {
        changeTime -= changeCoolTime;

        if(currentstats)
        {
            Skull2.SetActive(false);
            currentPlayer = csPlayer1;
            Skull1.SetActive(true);
            currentstats = true;
        }

        else
        {
            Skull1.SetActive(false);
            currentPlayer = csPlayer2;
            Skull2.SetActive(true);
            currentstats = false;
        }    
    }

    void Update()
    {
        
        PlayerChange();
    }

    void PlayerChange()
    {
        if (!currentstats && Input.GetKeyDown(KeyCode.Alpha1) && currentPlayer.IsJump && Time.time - changeTime >= changeCoolTime)
        {
            changeTime = Time.time;
            Skull2.SetActive(false);
            currentPlayer = csPlayer1;
            Skull1.transform.position = Skull2.transform.position + new Vector3(0f,0.38f, 0f);
            float dir = Skull1.transform.localScale.x > 0 ? 1f : -1f;
            Skull1.transform.localScale = new Vector3(4f * dir, 4f, 1f);
            currentstats = true;
            Skull1.SetActive(true);
        }

        else if (currentstats && Input.GetKeyDown(KeyCode.Alpha2) && currentPlayer.IsJump && Time.time - changeTime >= changeCoolTime)
        {
            changeTime = Time.time;
            Skull1.SetActive(false);
            currentPlayer = csPlayer2;
            Skull2.transform.position = Skull1.transform.position + new Vector3(0f, -0.38f, 0f);
            float dir = Skull1.transform.localScale.x > 0 ? 1f : -1f;
            Skull2.transform.localScale = new Vector3(3f * dir , 3f, 1f);
            currentstats = false;
            Skull2.SetActive(true);
        }
    }

}
