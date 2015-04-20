﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyERP.Web.Models.Factory
{
    public class ConsumptionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal PriceOfUnit { get; set; }
    }
}