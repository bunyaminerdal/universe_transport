using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private SolarSystem selectedSolarSystem;
    private Vector3 lastPosition;
    [SerializeField]
    private float cameraDepth = -500.0f;
    public SolarSystem SelectedSolarSystem { get => selectedSolarSystem; set { selectedSolarSystem = value; SelectSolarSystem(selectedSolarSystem); } }
    private Camera cameraMain;
    private void Awake()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SelectSolarSystem(SolarSystem solar)
    {
        lastPosition = transform.position;
        transform.position = new Vector3(solar.transform.position.x, cameraDepth, solar.transform.position.z);
        solar.ShowSystem(cameraDepth);
        cameraMain.cullingMask = ~0;

    }
}
