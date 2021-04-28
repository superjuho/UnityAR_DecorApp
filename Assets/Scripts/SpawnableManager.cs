using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class SwanableManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject spawnablePrefab;
    
    GameObject spawnedObject;
    

    Button button = GameObject.Find("Button").GetComponent<Button>();
    Button colorButton = GameObject.Find("ColorButton").GetComponent<Button>();

    public FlexibleColorPicker fcp;
    public Material material;
    public GameObject spawnableChair;

    public bool buttonPressed = false;
    public bool colorPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObject = null;
        button.onClick.AddListener(PressButton);
        colorButton.onClick.AddListener(ColorButton);
        
    }

    // Update is called once per frame
    void Update()
    {
        material.color = fcp.color;
        if (Input.touchCount == 0)
            return;
        
        if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
        {
            //if(Input.GetTouch(0).phase == TouchPhase.Began)
            if (buttonPressed)
            {
                Debug.Log("Painoit nappula");
                SpawnPrefab(m_Hits[0].pose.position);
                buttonPressed = false;
            } else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
            {
                spawnedObject.transform.position = m_Hits[0].pose.position;
            }
            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                spawnedObject = null;
            }
        }

        if(colorPressed)
        {

        }
    }

    private void SpawnPrefab(Vector3 spawnPosition)
    {
        if(buttonPressed) {
            spawnedObject = Instantiate(spawnablePrefab, spawnPosition, Quaternion.identity);
        } else if (colorPressed)
        {
            Debug.Log("pressed color button");
        }
    }
        

    private void PressButton()
    {
        buttonPressed = true;
    }
    
    private void ColorButton()
    {
        colorPressed = true;
    }
}
