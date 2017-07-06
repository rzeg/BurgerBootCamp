using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.VR.WSA.Input;

public class TapRecognizer : MonoBehaviour
{
    public GameObject target;
    public GameObject patty, salad, bread;
    private GestureRecognizer recognizer;
    private GameObject clonedObject;
    private bool toggle;

    // Use this for initialization
    void Start () {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += MyTapEventHandler;
        recognizer.StartCapturingGestures();
    }

    private void MyTapEventHandler(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        RaycastHit hit = new RaycastHit();

        Vector3 headPosition = Camera.main.transform.position;
        Vector3 gazeDirection = Camera.main.transform.forward;

        var direction = headRay.direction;
        var origin = headRay.origin;
        var position = origin + direction * 2.0f;
        GameObject prefab = salad;

        
        if (Physics.Raycast(headPosition, gazeDirection, out hit, 30.0f) && !toggle)
        {
            switch (hit.collider.tag)
            {
                case "Salad":
                    prefab = salad;
                    break;
                case "Patty":
                    prefab = patty;
                    break;
                case "Bread":
                    prefab = bread;
                    break;
            }

            toggle = true;
            clonedObject = Instantiate(prefab, hit.point, Camera.main.transform.localRotation);
        }
        else
        {
            toggle = false;
        }

}

    // Update is called once per frame
    void Update ()
    {

        if(toggle)
        {
            Quaternion toQuat = Camera.main.transform.localRotation;
            toQuat.x = 0;
            toQuat.z = 0;

            clonedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2.5f;
            clonedObject.transform.rotation = toQuat;
        }
	}
}
