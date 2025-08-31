using UnityEngine;

public class ChairLevelScript : MonoBehaviour
{
    public bool isExitDoor = false;
    public string theLevel;

    public void Level()
    {
        if (!isExitDoor)
        {
            FindAnyObjectByType<SceneManagerScript>().LoadScene(theLevel);
        }
    }

    public void Quit()
    {
        FindAnyObjectByType<SceneManagerScript>().QuitGame();
    }
}
