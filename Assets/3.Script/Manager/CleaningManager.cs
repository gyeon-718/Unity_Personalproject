using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaProgressManager : MonoBehaviour
{
    [System.Serializable]
    public class Area
    {
        public string areaName;
        public GameObject[] objectsToDestroy; // û���ؾߵ� ������Ʈ �迭
        public bool isCleared = false; // �ش� ���� Ŭ���� ����
    }

    public Area[] areas; // û�Ҹ� ������ ���� �迭
    //public Text progressText; 
    private int totalAreas;
    private int clearedAreas = 0;

    void Start()
    {
        totalAreas = areas.Length; // �� ���� ��
      //  UpdateProgress();
    }

    void Update()
    {
        CheckAllAreas(); // �� �����Ӹ��� ��� ������ ������Ʈ ���¸� üũ
    }

    private void CheckAllAreas()
    {
        clearedAreas = 0; // �ʱ�ȭ �� �ٽ� ī����
        foreach (Area area in areas)
        {
            if (CheckRemainingObjects(area) == 0)
            {
                area.isCleared = true; // �ش� ������ Ŭ�����
            }
            else
            {
                area.isCleared = false; // ���� Ŭ������� ����
            }

            if (area.isCleared)
            {
                clearedAreas++;
            }
        }
       // UpdateProgress(); // UI ���൵ ������Ʈ

        if (clearedAreas >= totalAreas)
        {
            Debug.Log("Game Clear! All areas cleared.");
          //  progressText.text = "Game Cleared!";
        }
    }

    private int CheckRemainingObjects(Area area)
    {
        int remaining = 0;
        foreach (GameObject obj in area.objectsToDestroy)
        {
            if (obj != null) // ���� �ı����� ���� ������Ʈ�� ������ ī��Ʈ ����
            {
                remaining++;
            }
        }
        return remaining;
    }

   // private void UpdateProgress()
   // {
   //     float progress = (float)clearedAreas / totalAreas * 100;
   //     progressText.text = "Total Progress: " + progress.ToString("F2") + "%";
   // }
}