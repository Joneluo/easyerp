﻿namespace EasyERP.Web.Validators.Products
{
    using EasyERP.Web.Framework.Validators;
    using EasyERP.Web.Models.Products;
    using FluentValidation;

    public class InventoryValidator : BaseValidator<InventoryModel>
    {
        public InventoryValidator()
        {
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("产品入库数量必须大于0");
            RuleFor(x => x.StoreId).GreaterThan(0).WithMessage("必须选择门店");
            RuleFor(x => x.Paid).GreaterThanOrEqualTo(0).WithMessage("已付款金额必须大于等于0");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("付款总金额必须大于等于0");
        }
    }
}