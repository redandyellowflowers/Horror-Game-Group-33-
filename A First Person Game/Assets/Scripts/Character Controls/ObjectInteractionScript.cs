using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ObjectInteractionScript : MonoBehaviour
{
    [Header("Small Interactions")]
    public float range = 10f;

    [Header("User Interface")]
    public Image reticle;

    private GameObject firstPersonCam;

    private void Awake()
    {
        firstPersonCam = GameObject.FindWithTag("MainCamera");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(firstPersonCam.GetComponent<Camera>().transform.position, firstPersonCam.GetComponent<Camera>().transform.forward, out hitInfo, range)
            && hitInfo.transform.gameObject.CompareTag("Interactable"))
        {
            reticle.color = Color.red;
        }
        else if (hitInfo.transform == null)
        {
            reticle.color = Color.white;
        }
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        RaycastHit hitInfo;

        if (!context.performed) return;

        if (Physics.Raycast(firstPersonCam.transform.position, firstPersonCam.transform.forward, out hitInfo, range))
        {
            TargetScript target = hitInfo.transform.GetComponent<TargetScript>();

            if (hitInfo.collider.CompareTag("Interactable") && target != null)
            {
                target.takeDamage(40);
            }
        }
    }
}
