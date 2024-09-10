using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager instance = null;
  [SerializeField]  private GameObject warningScreen;
    private PlayerStateMachine player;

    private void Start()
    {
        warningScreen.SetActive(false);
        player = FindObjectOfType<PlayerStateMachine>();
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Update()
    {
        if (player.isWarning) WarningScreen_Active();
        else WarningScreen_Disactive();
    }

    private void WarningScreen_Active()
    {
        warningScreen.SetActive(true);
    }

    private void WarningScreen_Disactive()
    {
        warningScreen.SetActive(false);
    }







}
