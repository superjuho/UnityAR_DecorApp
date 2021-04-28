using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class SpawnableManager : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    [SerializeField]
    GameObject colorPicker;
    [SerializeField]
    GameObject spawnablePrefab; // first chair *Fix to chairItemOne

    GameObject scrollMenu;
    GameObject spawnedObject;
    GameObject chosenObject;

    //HUD buttons
    Button colorButton;
    Button menuButton;
    Button deleteButton;

    //Scroll menu chair item buttons.
    Button ChairItemOne;
    Button ChairItemTwo;
    Button ChairItemThree;
    Button ChairItemFour;
    Button ChairItemFive;
    Button ChairItemSix;

    //Scroll menu couch item buttons.
    Button CouchItemOne;
    Button CouchItemTwo;
    Button CouchItemThree;
    Button CouchItemFour;
    Button CouchItemFive;
    Button CouchItemSix;

    //Scroll menu table item buttons.
    Button TableItemOne;
    Button TableItemTwo;
    Button TableItemThree;
    Button TableItemFour;
    Button TableItemFive;
    Button TableItemSix;


    public FlexibleColorPicker fcp;
    public Material material;

    //Prefabs for the chair models.
    public GameObject spawnableChair; // second chair *Fix to chairItemTwo
    public GameObject chairItemThree;
    public GameObject chairItemFour;
    public GameObject chairItemFive;
    public GameObject chairItemSix;

    //Prefabs for the couch models.
    public GameObject couchItemOne;
    public GameObject couchItemTwo;
    public GameObject couchItemThree;
    public GameObject couchItemFour;
    public GameObject couchItemFive;
    public GameObject couchItemSix;

    //Prefabs for the table models.
    public GameObject tableItemOne;
    public GameObject tableItemTwo;
    public GameObject tableItemThree;
    public GameObject tableItemFour;
    public GameObject tableItemFive;
    public GameObject tableItemSix;



    bool pickerShown = false;
    bool menuShown = false;
    bool objectChosen = false;

    // Start is called before the first frame update
    void Start()
    {
        //Locating and assigning HUD
        colorButton = GameObject.Find("ColorButton").GetComponent<Button>();
        colorButton.onClick.AddListener(ColorControl);
        colorButton.gameObject.SetActive(false);
        colorPicker.SetActive(false);

        menuButton = GameObject.Find("MenuButton").GetComponent<Button>();
        menuButton.onClick.AddListener(MenuControl);
        
        deleteButton.gameObject.SetActive(false);
        deleteButton = GameObject.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(DeleteObject);

        //Getting scrollmenu ready and hidden in the start
        scrollMenu = GameObject.Find("ScrollMenu");
        scrollMenu.SetActive(false);
        
        spawnedObject = null; //Nullllllllllllllllllllllllllllllllllll  boiii
        
        //Chair item button assignments
        ChairItemOne = GameObject.Find("Chair").GetComponent<Button>();
        ChairItemTwo = GameObject.Find("Rahi").GetComponent<Button>();
        ChairItemThree = GameObject.Find("ChairItemThree").GetComponent<Button>();
        ChairItemFour = GameObject.Find("ChairItemFour").GetComponent<Button>();
        ChairItemFive = GameObject.Find("ChairItemFive").GetComponent<Button>();
        ChairItemSix = GameObject.Find("ChairItemSix").GetComponent<Button>();

        //Chair items ObjectControl number assignments
        ChairItemOne.onClick.AddListener(delegate { ObjectControl(1); });
        ChairItemTwo.onClick.AddListener(delegate { ObjectControl(2); });
        ChairItemThree.onClick.AddListener(delegate { ObjectControl(3); });
        ChairItemFour.onClick.AddListener(delegate { ObjectControl(4); });
        ChairItemFive.onClick.AddListener(delegate { ObjectControl(5); });
        ChairItemSix.onClick.AddListener(delegate { ObjectControl(6); });

        //Couch item button assignments
        CouchItemOne = GameObject.Find("CouchItemOne").GetComponent<Button>();
        CouchItemTwo = GameObject.Find("CouchItemTwo").GetComponent<Button>();
        CouchItemThree = GameObject.Find("CouchItemThree").GetComponent<Button>();
        CouchItemFour = GameObject.Find("CouchItemFour").GetComponent<Button>();
        CouchItemFive = GameObject.Find("CouchItemFive").GetComponent<Button>();
        CouchItemSix = GameObject.Find("CouchItemSix").GetComponent<Button>();

        //Couch items ObjectControl number assignments
        CouchItemOne.onClick.AddListener(delegate { ObjectControl(7); });
        CouchItemTwo.onClick.AddListener(delegate { ObjectControl(8); });
        CouchItemThree.onClick.AddListener(delegate { ObjectControl(9); });
        CouchItemFour.onClick.AddListener(delegate { ObjectControl(10); });
        CouchItemFive.onClick.AddListener(delegate { ObjectControl(11); });
        CouchItemSix.onClick.AddListener(delegate { ObjectControl(12); });

        //Table item button assignments
        TableItemOne = GameObject.Find("TableItemOne").GetComponent<Button>();
        TableItemTwo = GameObject.Find("TableItemTwo").GetComponent<Button>();
        TableItemThree = GameObject.Find("TableItemThree").GetComponent<Button>();
        TableItemFour = GameObject.Find("TableItemFour").GetComponent<Button>();
        TableItemFive = GameObject.Find("TableItemFive").GetComponent<Button>();
        TableItemSix = GameObject.Find("TableItemSix").GetComponent<Button>();

        //Table items ObjectControl number assignments
        TableItemOne.onClick.AddListener(delegate { ObjectControl(13); });
        TableItemTwo.onClick.AddListener(delegate { ObjectControl(14); });
        TableItemThree.onClick.AddListener(delegate { ObjectControl(15); });
        TableItemFour.onClick.AddListener(delegate { ObjectControl(16); });
        TableItemFive.onClick.AddListener(delegate { ObjectControl(17); });
        TableItemSix.onClick.AddListener(delegate { ObjectControl(18); });
    }

    // Update is called once per frame
    void Update()
    {
        //Update color picked material color.
        material.color = fcp.color;

        if (Input.touchCount == 0)
            return;

        if (!menuShown)
        {
            if (m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits))
            {
                if(chosenObject != null && objectChosen)
                {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            if (spawnedObject != null)
                                Destroy(spawnedObject);

                            SpawnPrefab(m_Hits[0].pose.position);
                            colorButton.gameObject.SetActive(true);
                            deleteButton.gameObject.SetActive(true);

                        }
                        else if (Input.GetTouch(0).phase == TouchPhase.Moved && spawnedObject != null)
                        {
                            spawnedObject.transform.position = m_Hits[0].pose.position;
                        }
                        /*if (Input.GetTouch(0).phase == TouchPhase.Ended)
                        {
                            spawnedObject = null;
                        }*/ 
                        }
                }
        }
    }

        
    
    //Spawning prefab with assigned object from ObjectControl
    private void SpawnPrefab(Vector3 spawnPosition)
    {
            material = chosenObject.GetComponent<Renderer>().material;
            spawnedObject = Instantiate(chosenObject, spawnPosition, new Quaternion(0, -190, -180, 1));
        
    }

   /* private void ButtonPressed()
    {
        buttonPressed = true;
        colorButton.gameObject.SetActive(true);
    }*/

    private void ColorControl()
    {
        if (!pickerShown)
        {
            Debug.Log("nyt pit�is aueta v�ri ikkuna");
            colorPicker.SetActive(true);
            pickerShown = true;
            colorButton.GetComponentInChildren<Text>().text = "done";
            
        }
        else if (pickerShown)
        {
            colorPicker.SetActive(false);
            colorButton.GetComponentInChildren<Text>().text = "COLOR!!";
            pickerShown = false;
        }
    }

    /*private void ChairButtonPressed()
    {
        chairButtonPressed = true;
    }*/

    private void MenuControl()
    {
        if(!menuShown)
        {
            menuButton.gameObject.SetActive(false);
            scrollMenu.SetActive(true);
            menuShown = true;
        }
        else if (menuShown)
        {
            scrollMenu.SetActive(false);
            menuButton.gameObject.SetActive(true);
            menuShown = false;
        }
        
    }
    
    //ObjectControl controls the chosen model from the item menu.
    private void ObjectControl(int objectNumber)
    {
        MenuControl();
        int caseNumber = objectNumber;

        switch (caseNumber)
        {
            //Cases 1-6 chairs.
            case 1:
                chosenObject = spawnablePrefab;
                objectChosen = true;
                Debug.Log("painoit 1");
                break;

            case 2:
                chosenObject = spawnableChair;
                objectChosen = true;
                Debug.Log("painoit 2");
                break;

            case 3:
                chosenObject = chairItemThree;
                objectChosen = true;
                break;

            case 4:
                chosenObject = chairItemFour;
                objectChosen = true;
                break;

            case 5:
                chosenObject = chairItemFive;
                objectChosen = true;
                break;

            case 6:
                chosenObject = chairItemSix;
                objectChosen = true;
                break;
            
            //Cases 7-12 couches.
            case 7:
                chosenObject = couchItemOne;
                objectChosen = true;
                break;

            case 8:
                chosenObject = couchItemTwo;
                objectChosen = true;
                break;

            case 9:
                chosenObject = couchItemThree;
                objectChosen = true;
                break;

            case 10:
                chosenObject = couchItemFour;
                objectChosen = true;
                break;

            case 11:
                chosenObject = couchItemFive;
                objectChosen = true;
                break;

            case 12:
                chosenObject = couchItemSix;
                objectChosen = true;
                break;

            //Cases Table
            case 13:
                chosenObject = tableItemOne;
                objectChosen = true;
                break;

            case 14:
                chosenObject = tableItemTwo;
                objectChosen = true;
                break;

            case 15:
                chosenObject = tableItemThree;
                objectChosen = true;
                break;

            case 16:
                chosenObject = tableItemFour;
                objectChosen = true;
                break;

            case 17:
                chosenObject = tableItemFive;
                objectChosen = true;
                break;

            case 18:
                chosenObject = tableItemSix;
                objectChosen = true;
                break;

            default:
                Debug.Log("Ei voi tapahtua");
                break;
        }

    }

    private void DeleteObject()
    {
        if(spawnedObject != null)
        {
            Destroy(spawnedObject);
            deleteButton.gameObject.SetActive(false);
        }
    }
}
    