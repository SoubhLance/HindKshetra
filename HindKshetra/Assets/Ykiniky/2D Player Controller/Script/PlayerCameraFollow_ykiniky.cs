using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YkinikY
{
    public class PlayerCameraFollow_ykiniky : MonoBehaviour
    {
        [Header("(c) Ykiniky")]
        public bool followX;
        public bool followY;
        public bool zoomed = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector2 newCameraPosition = Vector2.Lerp(transform.position, FindAnyObjectByType<PlayerController_ykiniky>().transform.position, 0.15f);
            transform.position = new Vector3(followX ? newCameraPosition.x : 0, followY ? newCameraPosition.y : 0, -10);
            if (zoomed)
            {
                GetComponent<Camera>().orthographicSize = Vector2.Lerp(new Vector2(GetComponent<Camera>().orthographicSize, 0), new Vector2(5, 0), 0.03f).x;
            }
            else
            {
                GetComponent<Camera>().orthographicSize = Vector2.Lerp(new Vector2(GetComponent<Camera>().orthographicSize, 0), new Vector2(15, 0), 0.03f).x;
            }
        }
        public void FollowX()
        {
            followX = true;
        }
        public void FollowY()
        {
            followY = true;
        }
        public void DontFollowX()
        {
            followX = false;
        }
        public void DontFollowY()
        {
            followY = false;
        }
        public void Zoom()
        {
            zoomed = true;
        }
        public void Unzoom()
        {
            zoomed = false;
        }
    }

}