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
        public GameObject[] objectsToDestroy; // 청소해야될 오브젝트 배열
        public bool isCleared = false; // 해당 구역 클리어 여부
    }

    public Area[] areas; // 청소를 진행할 구역 배열
    //public Text progressText; 
    private int totalAreas;
    private int clearedAreas = 0;

    void Start()
    {
        totalAreas = areas.Length; // 총 구역 수
      //  UpdateProgress();
    }

    void Update()
    {
        CheckAllAreas(); // 매 프레임마다 모든 구역의 오브젝트 상태를 체크
    }

    private void CheckAllAreas()
    {
        clearedAreas = 0; // 초기화 후 다시 카운팅
        foreach (Area area in areas)
        {
            if (CheckRemainingObjects(area) == 0)
            {
                area.isCleared = true; // 해당 구역이 클리어됨
            }
            else
            {
                area.isCleared = false; // 아직 클리어되지 않음
            }

            if (area.isCleared)
            {
                clearedAreas++;
            }
        }
       // UpdateProgress(); // UI 진행도 업데이트

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
            if (obj != null) // 아직 파괴되지 않은 오브젝트가 있으면 카운트 증가
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