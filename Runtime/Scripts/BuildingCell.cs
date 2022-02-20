using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
    public class BuildingCell : MonoBehaviour
    {
        private List<Attachment> addedAttachments = new List<Attachment>();
        private List<AttachmentSlot> availableSlots = new List<AttachmentSlot>();
        private Dictionary<Attachment, GameObject> attachmentToGameobject = new Dictionary<Attachment, GameObject>();

        public void AddAtachment(Attachment _attachment,Vector3 objectPosition, Quaternion objectRotation)
        {
            if (!CanAddAttachment(_attachment)) return;

            addedAttachments.Add(_attachment);
            availableSlots.Remove(_attachment.OcupiedSlot);

            if (_attachment.EnabledSlots.Count > 0)
                availableSlots.AddRange(_attachment.EnabledSlots);

            GameObject instatiatedAttachment = Instantiate(_attachment.Prefab,objectPosition,objectRotation);
            attachmentToGameobject.Add(_attachment, instatiatedAttachment);
        }

        public void AddAtachmentAsExtension(BuildingCell _attachmentRoot, Attachment _attachment)
        {
            if (!CanAddAttachment(_attachment))
                return;
            addedAttachments.Add(_attachment);
            availableSlots.Remove(_attachment.OcupiedSlot);
            if (_attachment.EnabledSlots.Count > 0)
                availableSlots.AddRange(_attachment.EnabledSlots);
            attachmentToGameobject.Add(_attachment, _attachmentRoot.GetAttachmentGo(_attachment));
        }

        public GameObject GetAttachmentGo(Attachment _attachment) =>
            attachmentToGameobject[_attachment];

        public bool CanAddAttachment(Attachment _attachment) =>
            availableSlots.Contains(_attachment.OcupiedSlot);

    }
}
