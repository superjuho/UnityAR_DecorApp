using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARPlaneController : MonoBehaviour
{
    public GameObject Plane;
    public Button PlaneButton;
    bool PlaneShown = true;

    // Start is called before the first frame update
    void Start()
    {
        PlaneButton.onClick.AddListener(PlaneControl);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlaneControl()
    {
        if(Plane != null && PlaneShown)
        {
            Debug.Log("nyt ois plane hävinny");
            Plane.SetActive(false);
        }

        if (Plane != null && !PlaneShown)
        {
            Debug.Log("nyt ois plane taas näkyvillä");
            Plane.SetActive(true);
        } else if (Plane == null)
        {
            Debug.Log("juuuh");
        }
    }
}
