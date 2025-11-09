using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class File : BaseAuditableEntity
    {
        public string Filename { get; set; }
        public string EncryptedFilename { get; set; }
        public string Extension { get; set; }
        public long Size { get; set; }
        public string UploadFolder { get; set; }
        public FileType FileType { get; set; }
        public bool InTmp { get; set; }
        public FileOrigin FileOrigin { get; set; }
        public WorkOrder WorkOrder { get; set; }
        public Guid? WorkOrderId { get; set; }
        public WorkOrder WorkOrderAudioFile { get; set; }
        public WorkOrder WorkOrderVideoFile { get; set; }
        public Guid? StaffProfilePictureId { get; set; }
        public Staff StaffProfilePicture { get; set; }
    }

    public enum FileOrigin
    {
        Event = 1,
        Intervention = 2,
        SupplierArticle = 3,
        Staff = 4,
        SupplierInvoice = 5
    }

    public enum FileType
    {
        Audio = 1,
        Video = 2,
        Image = 3,
        Document = 4,
        Other = 5
    }
}
