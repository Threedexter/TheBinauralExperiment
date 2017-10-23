using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSHandler : MonoBehaviour
{
    #region Globals
    public Camera cam;
    [Range(0.0f, 2.0f)]
    public float MoveSpeed = 0.2f;
    [Range(1f, 10f)]
    public float lookSensitivity = 5f;

    float slowMoveSpeed;
    bool isSprinting;
    bool FreeLookEnabled = true;

    BaseInteractable interactable;

    public AudioSource winSource;
    #endregion

    #region UnityEngine
    void Start()
    {
        slowMoveSpeed = MoveSpeed;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
        Sprint();
        Interact();
    }
    #endregion

    #region Movement
    void Move()
    {
        Vector3 camVectorFwdLocked = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
        Vector3 camVectorRgtLocked = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
        transform.position = transform.position + (camVectorFwdLocked * Input.GetAxis("Vertical") * (isSprinting ? MoveSpeed * 10 : MoveSpeed));
        transform.position = transform.position + (camVectorRgtLocked * Input.GetAxis("Horizontal") * MoveSpeed);
    }

    void Look()
    {
        if (FreeLookEnabled)
        {
            if (cam.transform.eulerAngles.x <= 80 || cam.transform.eulerAngles.x >= -70)
            {
                cam.transform.eulerAngles += (new Vector3(Input.GetAxis("Mouse Y") * -1 * (lookSensitivity / 20), Input.GetAxis("Mouse X") * (lookSensitivity / 20), 0));
            }
            if (cam.transform.eulerAngles.x > 80 && cam.transform.eulerAngles.x < 150)
            {
                cam.transform.eulerAngles = (new Vector3(80, cam.transform.eulerAngles.y, 0));
            }
            if (cam.transform.eulerAngles.x < 290 && cam.transform.eulerAngles.x > 200)
            {
                cam.transform.eulerAngles = (new Vector3(290, cam.transform.eulerAngles.y, 0));
            }
            if (cam.transform.eulerAngles.z > 0)
            {
                cam.transform.eulerAngles = (new Vector3(cam.transform.eulerAngles.x, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z));
            }
        }
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
        }
    }
    #endregion

    #region Interact
    void Interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit hit;
            Physics.Raycast(cam.transform.position, cam.transform.forward, out hit);
            if (hit.collider.gameObject.tag == "Interactible")
            {
                GetBaseInteractable(hit);
            }
            if (interactable != null && interactable.getWithinRange())
            {
                FreeLookEnabled = false;
                interactable.OnInteract(true);
            }
        }
        if (Input.GetButtonUp("Interact"))
        {
            if (interactable == null)
            {
                return;
            }
            interactable.OnInteract(false);
            FreeLookEnabled = true;
        }
    }

    public void WinPuzzle()
    {
        if (!winSource.isPlaying)
            winSource.Play();
    }

    void GetBaseInteractable(RaycastHit hit)
    {
        interactable = hit.collider.gameObject.GetComponent<BaseInteractable>();
        if (interactable == null)
        {
            interactable = hit.collider.gameObject.GetComponentInParent<BaseInteractable>();
            if (interactable == null)
            {
                Debug.Log("Interactable is null");
                Debug.Log("Name: " + hit.collider.gameObject.name);
                return;
            }
        }
    }
    #endregion
}
