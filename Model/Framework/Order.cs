namespace Model.Framework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [Key]
        public int OrderDetailsId { get; set; }

        [StringLength(450)]
        public string OrderNo { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public int? UserId { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        public int? PaymentId { get; set; }

        public DateTime? OrderDate { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
