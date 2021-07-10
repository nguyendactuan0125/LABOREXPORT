namespace Service.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(250)]
        public string Password { get; set; }

        [StringLength(50)]
        public string UserRoles { get; set; }

        [StringLength(10)]
        public string DepartmentCode { get; set; }

        [StringLength(10)]
        public string CompanyCode { get; set; }

        public int? Approved { get; set; }

        public int? Status { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
