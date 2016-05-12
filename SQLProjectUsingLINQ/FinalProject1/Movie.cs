//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FinalProject1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Movie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Movie()
        {
            this.Filmographies = new HashSet<Filmography>();
            this.MovieSchedules = new HashSet<MovieSchedule>();
        }
    
        public int MovieID { get; set; }
        public string Title { get; set; }
        public Nullable<int> LengthInMinutes { get; set; }
        public string Rating { get; set; }
        public Nullable<System.DateTime> ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Description { get; set; }
        public Nullable<int> GenreID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Filmography> Filmographies { get; set; }
        public virtual Genre Genre { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MovieSchedule> MovieSchedules { get; set; }
    }
}
