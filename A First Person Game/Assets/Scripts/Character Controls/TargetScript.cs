using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private int health = 80;

    public void takeDamage(int amount)
    {
        health -= amount;

        if (health == 80)
        {
            gameObject.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
            FindAnyObjectByType<AudioManagerScript>().Play("Green & Yellow");
        }

        if (health == 40)
        {
            gameObject.transform.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            FindAnyObjectByType<AudioManagerScript>().Play("Green & Yellow");
        }

        if (health == 0)
        {
            gameObject.transform.gameObject.tag = "Untagged";
            gameObject.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;

            gameObject.transform.GetChild(0).gameObject.SetActive(false);

            FindAnyObjectByType<AudioManagerScript>().Play("Red");
        }
    }
}
