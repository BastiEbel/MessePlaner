//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MessePlaner
{
    using System;
    using System.Collections.Generic;
    
    public partial class Exponat
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Exponat()
        {
            this.Maße = new HashSet<Maße>();
            this.Retoure = new HashSet<Retoure>();
            this.Versand = new HashSet<Versand>();
            this.Zubehör = new HashSet<Zubehör>();
        }
    
        public int Exponat_ID { get; set; }
        public string Exponatname { get; set; }
        public int Exponatnummer { get; set; }
        public string Exponatzubehör { get; set; }
        public Nullable<int> Anzahl { get; set; }
        public Nullable<decimal> Exponatversion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Maße> Maße { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Retoure> Retoure { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Versand> Versand { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zubehör> Zubehör { get; set; }
    }
}
