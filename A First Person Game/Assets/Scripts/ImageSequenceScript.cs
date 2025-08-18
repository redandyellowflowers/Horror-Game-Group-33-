using UnityEngine;

public class ImageSequenceScript : MonoBehaviour
{
    /*
    1.
    Title: How to change UI image Array with button in Unity | Unity Tutorial
    Author: Grafik Games
    Date: 16 August 2025
    Code version: 1
    Availability: https://www.youtube.com/watch?v=UQ7FjIwbJsA&list=WL&index=3

    2.
    Title: "Sophies Heroic Bloodbath - Assignment 4 - Exam - Playable Game"
    Author: Miguel Marindanise, Fatima Zahraa Bham, Yongama Ntloko 
    Date: 16 August 2025
    Code version: 1
    */

    [Header("gameObjects")]
    public GameObject[] image;

    [Header("previous buttons")]
    public GameObject previousButton;

    [Header("next buttons")]
    public GameObject nextButton;

    [Header("continue buttons")]
    public GameObject nextLevelButton;

    private int indexAmount;
    private int index;

    private void Awake()
    {
        indexAmount = image.Length;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= indexAmount)
        {
            index = indexAmount;//resets the index when it has been completed
        }

        if (index < 0)
        {
            index = 0;//sets the index to 0, thus ensuring it never enters the negative
            previousButton.transform.parent.gameObject.SetActive(false);
        }

        if (index == 1)//if it is the image after the first, the "previous" button is enabled
        {
            previousButton.SetActive(true);
        }

        if (index == indexAmount - 1)//if it is the last image in the sequence, then the next button is disabled, and the continue button is enabled
        {
            if (nextLevelButton != null)
            {
                nextLevelButton.SetActive(true);
            }

            nextButton.SetActive(false);//disables the "next" button as there are no more images in index
        }
        else if (index < indexAmount)
        {
            if (nextLevelButton != null)//if there are still images in index, the "continue" button is disabled
            {
                nextLevelButton.SetActive(false);
            }

            nextButton.SetActive(true);
        }

        if (index == 0)
        {
            image[0].gameObject.SetActive(true);//if the index is at 0, return to the first image
        }
    }

    public void Next()
    {
        index += 1;//adds 1 to the index

        for (int i = 0; i < image.Length; i++)//if [i] is less than the max number of images in the index, add 1 to it
        {
            image[i].gameObject.SetActive(false);//turns off current image, and enables the next
            image[index].gameObject.SetActive(true);//enables the next in the index, after 1 is added to the current
        }
    }

    public void ResetSequence()
    {
        index = 0;
    }

    public void Previous()
    {
        index -= 1;//does the same as the above ("next()") but in reverse, this time, subtracting 1, thus returning to the previous image

        for (int i = 0; i < image.Length; i++)
        {
            image[i].gameObject.SetActive(false);
            image[index].gameObject.SetActive(true);
        }
    }
}
