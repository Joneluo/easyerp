﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    class ProductStatisticConfiguration : EntityTypeConfiguration<ProductStatistic>
    {
        public ProductStatisticConfiguration()
        {
            this.HasKey(p => p.Id);
            this.Property(p => p.QualifyQuaitity);
            this.Property(p => p.QualifyQuaitity);
            this.HasRequired(p => p.Product).WithMany(o => o.ProduceRecord).HasForeignKey(p => p.Upc);
        }
    }
}
