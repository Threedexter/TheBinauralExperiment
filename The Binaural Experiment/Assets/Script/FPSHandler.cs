using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSHandler : MonoBehaviour
{
    #region Globals
    public Camera cam;
    [Range(0.0f, 100.0f)]
    public float MoveSpeed = 0.2f;
    [Range(1f, 10f)]
    public float lookSensitivity = 5f;

    float slowMoveSpeed;
    bool isSprinting;
    bool FreeLookEnabled = true;
    bool canMove = true;

    float alpha = 0f;
    private float fadeDir = -0f;
    public float fadeSpeed = 0.3f;
    public Image panel;

    Rigidbody playerRigid;

    float startY;

    BaseInteractable interactable;

    public AudioSource winSource;
    #endregion

    #region UnityEngine
    void Start()
    {
        slowMoveSpeed = MoveSpeed;
        Cursor.visible = false;
        startY = transform.position.y;
        playerRigid = GetComponent<Rigidbody>();
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
        if (canMove)
        {
            Vector3 camVectorFwdLocked = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
            Vector3 camVectorRgtLocked = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0)
            {
                playerRigid.AddForce((camVectorFwdLocked * Input.GetAxis("Vertical") * (isSprinting ? MoveSpeed * 10 : MoveSpeed)), ForceMode.VelocityChange);
                //transform.position = transform.position + (camVectorFwdLocked * Input.GetAxis("Vertical") * (isSprinting ? MoveSpeed * 10 : MoveSpeed));
            }
            if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0)
            {
                playerRigid.velocity = new Vector3(playerRigid.velocity.x, 0, 0);
                playerRigid.angularVelocity = new Vector3(playerRigid.angularVelocity.x, 0, 0);
            }
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
            {
                playerRigid.AddForce((camVectorRgtLocked * Input.GetAxis("Horizontal") * (isSprinting ? MoveSpeed * 10 : MoveSpeed)), ForceMode.VelocityChange);
                //transform.position = transform.position + (camVectorRgtLocked * Input.GetAxis("Horizontal") * MoveSpeed);
            }
            if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0)
            {
                playerRigid.velocity = new Vector3(0, 0, playerRigid.velocity.z);
                playerRigid.angularVelocity = new Vector3(0, 0, playerRigid.angularVelocity.z);
            }
        }

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

    void OnCollisionEnter(Collision col)
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        StartCoroutine(ResetConstraints());
    }

    IEnumerator ResetConstraints()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionExit(Collision col)
    {
        Debug.Log("Stopped Hit");
    }

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

    public void setCanMove(bool value)
    {
        canMove = value;
    }

    public bool getCanMove()
    {
        return canMove;
    }

    public void fadeCameraOut()
    {
        fadeDir = 1;
        alpha = 0;
    }

    void OnGUI()
    {
        if (panel != null)
        {
            alpha += fadeDir * fadeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);

            panel.color = new Color(0, 0, 0, alpha);
        }
    }

    public float getAlpha()
    {
        return alpha;
    }
}
