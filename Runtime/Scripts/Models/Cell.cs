using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
    public class Cell
    {
        public Vector2Int Coordinates;

        private List<Attachment> _attachments;
        private List<AttachmentSlot> _ocupiedSlots;
        private List<AttachmentSlot> _enabledSlots;

        public Cell(int x, int y)
        {
            Coordinates.x = x;
            Coordinates.y = y;

            _attachments = new List<Attachment>();
            _ocupiedSlots = new List<AttachmentSlot>();
            _enabledSlots = new List<AttachmentSlot>();
        }

        public bool ContainsEnablesSlot(AttachmentSlot attachmentSlot)
        {
            return _enabledSlots.Contains(attachmentSlot);
        }
        
        public bool ContainsOcupiedSlot(AttachmentSlot attachmentSlot)
        {
            return _ocupiedSlots.Contains(attachmentSlot);
        }

        public void AddAttachement(Attachment attachment)
        {
            _attachments.Add(attachment);

            if(!_ocupiedSlots.Contains(attachment.OcupiedSlot))
                _ocupiedSlots.Add(attachment.OcupiedSlot);

            foreach (var item in attachment.EnabledSlots)
            {
                if(!_enabledSlots.Contains(item))
                    _enabledSlots.Add(item);
            }
        }

        public void RemoveAttachment(Attachment attachment)
        {
            if (_attachments.Contains(attachment))
                _attachments.Remove(attachment);


            if (_ocupiedSlots.Contains(attachment.OcupiedSlot))
                _ocupiedSlots.Remove(attachment.OcupiedSlot);

            foreach (var item in attachment.EnabledSlots)
            {
                if (_enabledSlots.Contains(item))
                    _enabledSlots.Remove(item);
            }
        }
    }
}