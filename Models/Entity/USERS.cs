
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace POStock.Models.Entity
{

using System;
    using System.Collections.Generic;
    
public partial class USERS
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public USERS()
    {

        this.ALIS = new HashSet<ALIS>();

        this.KATEGORI = new HashSet<KATEGORI>();

        this.MUSTERI = new HashSet<MUSTERI>();

        this.SATIS = new HashSet<SATIS>();

        this.URUN = new HashSet<URUN>();

    }


    public short UserID { get; set; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string Salt { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<ALIS> ALIS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<KATEGORI> KATEGORI { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<MUSTERI> MUSTERI { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<SATIS> SATIS { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<URUN> URUN { get; set; }

}

}
