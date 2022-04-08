using UnityEngine;

namespace ATH.HouseBuilding
{
    public class CameraController : MonoBehaviour
    {
        public float roataionSpeed = 1;
        public float panningSpeed = 1;

        private BaseInput input;
        private bool rotate;
        private bool pan;
        private bool verticalPan;
        private bool isEnabled = true;
        private void Awake()
        {
            input = new BaseInput();
        }
        private void OnEnable()
        {
            input.Enable();
        }
        private void Start()
        {
            input.CameraController.EnableRotate.started += ctx => { rotate = true; Cursor.lockState = CursorLockMode.Locked; };
            input.CameraController.EnableRotate.canceled += ctx => { rotate = false; Cursor.lockState = CursorLockMode.None; };
            input.CameraController.Pan.started += ctx => { pan = true; };
            input.CameraController.Pan.canceled += ctx => { pan = false; };
            input.CameraController.VericalPan.started += ctx => { verticalPan = true; };
            input.CameraController.VericalPan.canceled += ctx => { verticalPan = false; };
            input.CameraController.EnableCameraController.performed += ctx => { isEnabled = !isEnabled; };
        }
        private void Update()
        {
            if(!isEnabled)
            {
                return;
            }
            if (rotate)
            {
                RotateCamera();
            }
            if (pan || verticalPan)
            {
                PanCamera();
            }
        }
        private void PanCamera()
        {
            float verticalInput = input.CameraController.VericalPan.ReadValue<float>() * panningSpeed;
            Vector2 inputValue = input.CameraController.Pan.ReadValue<Vector2>() * panningSpeed;
            Vector3 movementVector = transform.position + transform.right * inputValue.x + transform.forward * inputValue.y + transform.up * verticalInput;
            transform.position = Vector3.Lerp(transform.position, movementVector, Time.deltaTime);
        }
        private void RotateCamera()
        {
            Vector2 inputValue = input.CameraController.Rotation.ReadValue<Vector2>();
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * (inputValue.x * roataionSpeed * Time.deltaTime) + Vector3.right * (-inputValue.y * roataionSpeed * Time.deltaTime));
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles +
            //    Vector3.up * (inputValue.x * roataionSpeed * Time.deltaTime) + Vector3.right * (inputValue.y * roataionSpeed * Time.deltaTime)), 0.15f);
        }
        private void OnDisable()
        {
            input.Disable();
        }
    }
}
