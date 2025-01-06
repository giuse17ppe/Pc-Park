using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class PlayerController : MonoBehaviour, IDataPersistence
{
    public float speed = 5.0f;
    public float mouseSensitivity;
    private float initY;
    private float xRotation = 0f;
    private CharacterController controller;
    private Camera cameraChild;







    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        initY = transform.position.y;
        cameraChild = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();


    }

    // Update is called once per frame
    void Update()
    {
        if (!ConversationManager.Instance.IsConversationActive) //non ti puoi muovere se una conversazione è attiva 
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * Time.deltaTime * speed);

            Vector3 currentPosition = transform.position;
            currentPosition.y = initY;
            transform.position = currentPosition;

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            cameraChild.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }

    }
    

  


    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
        this.transform.rotation = data.playerRotation;

    }

    public void SaveData(GameData data)
    {
        data.playerPosition = this.transform.position;
        data.playerRotation = this.transform.rotation;

    }



}
