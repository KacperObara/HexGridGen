using UnityEngine;
using HexGen;

namespace HexGenExampleGame1
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private MapSettings mapSettings;

        public float panSpeed = 20f;
        public float panBorderThickness = 10f;
        public float scrollSpeed = 20f;
        public float minY = 20f;
        public float maxY = 300f;

        public bool EdgeMoving = false;

        void Update()
        {
            Vector3 pos = transform.position;

            if (Input.GetKey("w") || (EdgeMoving && Input.mousePosition.y >= Screen.height - panBorderThickness))
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || (EdgeMoving && Input.mousePosition.y <= panBorderThickness))
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || (EdgeMoving && Input.mousePosition.x <= panBorderThickness))
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || (EdgeMoving && Input.mousePosition.x >= Screen.width - panBorderThickness))
            {
                pos.x += panSpeed * Time.deltaTime;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, minY, maxY);

            pos.x = Mathf.Clamp(pos.x, 0, mapSettings.GetRealWorldWidth());
            pos.z = Mathf.Clamp(pos.z, 0, mapSettings.GetRealWorldHeight());

            transform.position = pos;
        }
    }
}
