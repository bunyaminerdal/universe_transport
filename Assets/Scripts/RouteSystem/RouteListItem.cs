using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RouteListItem : MonoBehaviour
{
    Route route;
    [SerializeField] TMP_Text routeName;
    [SerializeField] TMP_Text routeVehicles;
    [SerializeField] TMP_Text routeBalance;
    [SerializeField] Image routeImage;

    private void OnEnable()
    {
        transform.GetComponent<Toggle>().onValueChanged.AddListener(clickedEvent);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDisable()
    {
        transform.GetComponent<Toggle>().onValueChanged.RemoveListener(clickedEvent);
    }

    public void InitializeItem(Route _route)
    {
        route = _route;
        routeName.text = _route.RouteName;
        routeImage.color = _route.RouteColor;
        routeVehicles.text = _route.TransportVehicles.Count.ToString();
        routeBalance.text = "1000000";
        transform.GetComponent<Toggle>().isOn = true;
    }
    public void UpdateItem(Route _route)
    {
        routeName.text = _route.RouteName;
        routeImage.color = _route.RouteColor;
        routeVehicles.text = _route.TransportVehicles.Count.ToString();
        routeBalance.text = "1000000";
    }

    private void clickedEvent(bool isOn)
    {
        UIEventHandler.SingleRouteItemClickedEvent?.Invoke(route, isOn);
    }
}
