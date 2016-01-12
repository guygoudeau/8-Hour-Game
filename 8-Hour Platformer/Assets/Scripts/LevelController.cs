using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
    public static int currentScore;
    public static int highScore;

    public static int currentLevel = 0;
    public static int unlockedLevel;

    public static void CompleteLevel()
    {
        if (currentLevel < 4)
        {
            currentLevel += 1;
            Application.LoadLevel(currentLevel);
        }

        else
        {
            print("you win");
        }
        
    }
}
