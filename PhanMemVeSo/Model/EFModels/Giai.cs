//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EFModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Giai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Giai()
        {
            this.KetQuaXoSoes = new HashSet<KetQuaXoSo>();
        }
    
        public int GiaiId { get; set; }
        public string TenGiai { get; set; }
        public decimal TienThuong { get; set; }
        public int SLSoTrung { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaXoSo> KetQuaXoSoes { get; set; }
    }
}
