using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController main; // 싱글톤

    private int currentLevel;
    
    public int CurrentLevel
    {
        get { return currentLevel; }
        set { currentLevel = value; }
    }

    private void Awake()
    {
        main = this;
    }

    /// <summary>
    /// 현재 레벨에 맞는 플레이어 스피드를 반환합니다.
    /// </summary>
    /// <returns>플레이어 스피드</returns>
    public float GetPlayerSpeed()
    {
        switch (currentLevel)
        {
            case 1:
                return 10;
            case 2:
                return 7;
            case 3:
                return 5;
            case 4:
                return 3;
            default:
                return 0;
        }
    }
}
