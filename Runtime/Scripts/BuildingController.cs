using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
    public class BuildingController : MonoBehaviour
    {
        public bool useMouseInput = false;
        public Material canConstructMat;
        public Material invalidConstructMat;
        public AttachmentSet attachmentSet;
        public LayerMask buildingPartsLayer;
        //DEBUG DATA
        public GameObject floorPrefab;
        public bool[,] gridShape = new bool[10, 10]
        {
            {true,true,true,true,true,true,true,true,true,true },
            {true,true,true,true,true,true,true,true,true,true },

            {true,true,true,true,false,false,true,true,true,true },
            {true,true,true,true,false,false,true,true,true,true },
            {true,true,true,true,false,false,true,true,true,true },
            {true,true,true,true,false,false,true,true,true,true },
            {true,true,true,true,false,false,true,true,true,true },
            {true,true,true,true,false,false,true,true,true,true },

            {true,true,true,true,true,true,true,true,true,true },
            {true,true,true,true,true,true,true,true,true,true }
        };
        public string buildingName;
        //END 

        private CoreGrid buildingGrid;
        private BaseInput baseInput;
        private GameObject previewObject;
        private GameObject objectForConstruction;
        private Renderer[] previewObjectRenderes;
        private Attachment selectedAttachment;
        private Dictionary<BuildingCell, Cell> buildingCellToCell;
        private Dictionary<Cell, BuildingCell> cellToBuildingCell;
        private Dictionary<GameObject, Cell> goToCell;
        private Vector2Int selectedCoord = Vector2Int.zero;
        private Quaternion currentRotation;
        private Transform lastObjectRaycasted;
        private Camera mainCamera;

        private bool canConstruct;

        private const float kVerticalOffset = 0.1f;

        private void OnEnable()
        {
            baseInput = new BaseInput();
            baseInput.Enable();
        }

        private void Awake()
        {
            cellToBuildingCell = new Dictionary<Cell, BuildingCell>();
            buildingCellToCell = new Dictionary<BuildingCell, Cell>();
            goToCell = new Dictionary<GameObject, Cell>();
            buildingGrid = new CoreGrid(gridShape, transform.position, transform.rotation, buildingName);
            mainCamera = Camera.main;
        }

        private void Start()
        {
            Create(buildingGrid);
            InitEdit();
            currentRotation = Quaternion.identity;
            SetNewPreviewObject(attachmentSet.attachments[0]);
        }

        private void Update()
        {
            if (useMouseInput)
            {
                HandleRaycast();
            }
        }

        private void OnDisable()
        {
            baseInput.Disable();
        }

        private void Create(CoreGrid targetGrid)
        {
            GameObject rootObject = new GameObject(targetGrid.GridId);
            rootObject.transform.position = targetGrid.Position;
            rootObject.transform.rotation = targetGrid.Roation;
            for (int i = 0; i < targetGrid.Width; i++)
            {
                for (int j = 0; j < targetGrid.Height; j++)
                {
                    if (targetGrid[i,j] != null)
                    {
                        Vector3 instatiationPosition = rootObject.transform.position + rootObject.transform.forward * i + rootObject.transform.right * j;
                        BuildingCell currentBuildingCell = Instantiate(floorPrefab, instatiationPosition, rootObject.transform.rotation, rootObject.transform).GetComponent<BuildingCell>();
                        cellToBuildingCell.Add(targetGrid[i, j], currentBuildingCell);
                        buildingCellToCell.Add(currentBuildingCell, targetGrid[i, j]);
                        goToCell.Add(currentBuildingCell.gameObject, targetGrid[i, j]);
                    }
                }
            }
        }

        private void InitEdit()
        {
            baseInput.BuildingEditing.SelectUpperCell.performed += ctx => { UpdateSelectedCoord(false, 1, buildingGrid.Height - 1); };
            baseInput.BuildingEditing.SelectLowerCell.performed += ctx => { UpdateSelectedCoord(false, -1, buildingGrid.Height - 1); };
            baseInput.BuildingEditing.SelectRightCell.performed += ctx => { UpdateSelectedCoord(true, 1, buildingGrid.Width - 1); };
            baseInput.BuildingEditing.SelectLeftCell.performed += ctx => { UpdateSelectedCoord(true, -1, buildingGrid.Width - 1); };
            baseInput.BuildingEditing.Rotate.performed += ctx => { currentRotation.y = (currentRotation.y + 90) % 360; UpdatePreview(); };
            baseInput.BuildingEditing.Construct.performed += ctx => { AttemptConstruct(); };
        }

        private void UpdateSelectedCoord(bool isXAxis, int increment, int dimension)
        {
            //should make it look better
            if (useMouseInput) return;
            if (isXAxis)
            {
                if (buildingGrid[selectedCoord.y, selectedCoord.x + increment] == null)
                {
                    return;
                }
                selectedCoord.x = Mathf.Clamp(selectedCoord.x + increment, 0, dimension);
            }
            else
            {
                if (buildingGrid[selectedCoord.y + increment, selectedCoord.x] == null)
                {
                    return;
                }
                selectedCoord.y = Mathf.Clamp(selectedCoord.y + increment, 0, dimension);
            }
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            previewObject.transform.position = GetObjectToBePlacedPosition();
            previewObject.transform.rotation =buildingGrid.Roation * currentRotation;
            canConstruct = cellToBuildingCell[buildingGrid[selectedCoord.y, selectedCoord.x]].CanAddAttachment(selectedAttachment);
            foreach (var item in previewObjectRenderes)
            {
                item.material = canConstruct ? canConstructMat : invalidConstructMat;
            }
        }

        private void AttemptConstruct()
        {
            if (!canConstruct) return;
            cellToBuildingCell[buildingGrid[selectedCoord]].AddAtachment(selectedAttachment, GetObjectToBePlacedPosition() - Vector3.up / 10,buildingGrid.Roation * currentRotation);
            UpdatePreview();
        }

        private Vector3 GetObjectToBePlacedPosition() =>
            cellToBuildingCell[buildingGrid[selectedCoord]].transform.position + Vector3.up * kVerticalOffset;

        private void HandleRaycast()
        {
            Ray ray = mainCamera.ScreenPointToRay(baseInput.BuildingEditing.MousePosition.ReadValue<Vector2>());
            //do some checks for correct type of object
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 15f, buildingPartsLayer))
            {
                //check for a script
                if (lastObjectRaycasted != hitInfo.transform)
                {
                    lastObjectRaycasted = hitInfo.transform;
                    Cell currentSelectedCell = goToCell[hitInfo.transform.gameObject];
                    //selectedCoord.x = currentSelectedCell.Coordinates.y;
                    //selectedCoord.y = currentSelectedCell.Coordinates.x;
                    UpdatePreview();
                }
            }
        }

        public void SetNewPreviewObject(Attachment newAttachment)
        {
            Destroy(previewObject);
            selectedAttachment = newAttachment;
            previewObject = Instantiate(newAttachment.Prefab, Vector3.zero, Quaternion.identity);
            objectForConstruction = newAttachment.Prefab;
            previewObjectRenderes = previewObject.GetComponentsInChildren<Renderer>();
            UpdatePreview();
        }
    }
}
