using System.Collections.Generic;
using UnityEngine;

namespace ATH.HouseBuilding
{
    [CreateAssetMenu]
    public class AttachmentSet : ScriptableObject
    {
        public List<Attachment> attachments;
        public Dictionary<AttachmentSlot, List<Attachment>> tagToAttachement;

        public void Refresh()
        {
            tagToAttachement = new Dictionary<AttachmentSlot, List<Attachment>>();
         
            foreach (Attachment item in attachments)
            {
                if (item == null) continue;
                tagToAttachement[item.OcupiedSlot].Add(item);
            }
        }
    }
}
