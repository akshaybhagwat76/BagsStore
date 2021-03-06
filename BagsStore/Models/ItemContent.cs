//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BagsStore.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ItemContent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ItemContent()
        {
            this.Items = new HashSet<Item>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public string SortDescription { get; set; }
        public string Content { get; set; }
        public string SmallImage { get; set; }
        public string MediumImage { get; set; }
        public string BigImage { get; set; }
        public Nullable<long> NumOfView { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string SecondTitle { get; set; }
        public string ImageDescription { get; set; }
        public Nullable<bool> IsFree { get; set; }
        public Nullable<int> GaleryId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Item> Items { get; set; }
    }
}
