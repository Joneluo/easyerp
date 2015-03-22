﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntityFramework.Configurations.Factory
{
    using System.Data.Entity.ModelConfiguration;
    using Domain.Model.Factory;

    class AttritionStatisticConfiguration : EntityTypeConfiguration<AttritionStatistic>
    {
        public AttritionStatisticConfiguration()
        {
            this.HasKey(o => o.Id);
            this.Property(o => o.Date);
            this.Property(o => o.Volume);
            this.Property(o => o.PriceOfUnit);
            this.HasRequired(o => o.Attrition).WithMany(a => a.AttritionRecords).HasForeignKey(o => o.AttritionId);
        }
    }
}
