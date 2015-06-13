﻿using System;

namespace Doamin.Service.ExportImport
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using Doamin.Service.Products;
    using Domain.Model.Products;
    using EasyErp.Core;

    public class ExportManager : IExportManager
    {
        private readonly ICategoryService categoryService;
        private readonly IProductPriceService productPriceService;
        private readonly IInventoryService inventoryService;
        private IWorkContext workContext;

        public ExportManager(ICategoryService categoryService, IProductPriceService priceService, IInventoryService inventoryService, IWorkContext workContext)
        {
            this.categoryService = categoryService;
            this.productPriceService = priceService;
            this.inventoryService = inventoryService;
            this.workContext = workContext;
        }

        public string ExportProductsToXml(IList<Product> products)
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            {
                using (var xmlWriter = new XmlTextWriter(stringWriter))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("Root");
                    xmlWriter.WriteAttributeString("Version", EasyErpVersion.CurrentVersion);
                    var categories = categoryService.GetAllCategories();
                    xmlWriter.WriteStartElement("Categories");
                    foreach (var category in categories)
                    {
                        xmlWriter.WriteStartElement("Category");
                        xmlWriter.WriteElementString("Name", string.Empty, category.Name);
                        xmlWriter.WriteElementString("ItemNo", string.Empty, category.ItemNo);
                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("Products");
                    foreach (var product in products)
                    {
                        var price = productPriceService.GetProductPrice(product.Id, workContext.CurrentUser.StoreId);
                        xmlWriter.WriteStartElement("Product");

                        xmlWriter.WriteElementString("ProductId", string.Empty, product.Id.ToString());
                        xmlWriter.WriteElementString("Name", string.Empty, product.Name);
                        xmlWriter.WriteElementString("ItemNo", string.Empty, product.ItemNo);
                        xmlWriter.WriteElementString("ShortDescription", string.Empty, product.ShortDescription);
                        xmlWriter.WriteElementString("FullDescription", string.Empty, product.FullDescription);
                        xmlWriter.WriteElementString("Gtin", string.Empty, product.Gtin);
                        xmlWriter.WriteElementString("StockQuantity", String.Empty, inventoryService.GetProductQuantity(product.Id, workContext.CurrentUser.StoreId).ToString());
                        xmlWriter.WriteElementString("Price", string.Empty, price.SalePrice.ToString());
                        xmlWriter.WriteElementString("ProductCost", string.Empty, price.CostPrice.ToString());
                        xmlWriter.WriteElementString("Weight", string.Empty, product.Weight.ToString());
                        xmlWriter.WriteElementString("Length", string.Empty, product.Length.ToString());
                        xmlWriter.WriteElementString("Width", string.Empty, product.Width.ToString());
                        xmlWriter.WriteElementString("Height", string.Empty, product.Height.ToString());
                        xmlWriter.WriteElementString("Published", string.Empty, product.Published.ToString());
                        xmlWriter.WriteElementString("CreatedOnUtc", string.Empty, product.CreatedOnUtc.ToString());
                        xmlWriter.WriteElementString("UpdatedOnUtc", string.Empty, product.UpdatedOnUtc.ToString());
                        xmlWriter.WriteElementString("Category", string.Empty, product.Category.Name);
                        xmlWriter.WriteElementString("CategoryNo", string.Empty, product.Category.ItemNo);
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                    return stringWriter.ToString();
                }
            }
        }
    }
}