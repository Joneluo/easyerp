﻿using System;
using System.Collections.Generic;

namespace Doamin.Service.Factory
{
    using System.Linq;
    using Domain.Model.Factory;
    using Infrastructure.Domain;
    using Infrastructure.Utility;

    public class ConsumptionTimesheetService : TimesheetService<Consumption, ConsumptionStatistic>
    {
        private readonly IConsumptionService consumptionService;

        private readonly IRepository<ConsumptionStatistic> repository;
  
        public ConsumptionTimesheetService(IUnitOfWork unitOfWork, IConsumptionService consumptionService,
            IRepository<ConsumptionStatistic> repository)
            : base(unitOfWork)
        {
            this.consumptionService = consumptionService;
            this.repository = repository;
        }

        protected override PagedResult<Consumption> GetCategories(int page, int pageSize)
        {
            return this.consumptionService.GetConsumptionCategories(page, pageSize);
        }

        protected override IEnumerable<ConsumptionStatistic> GetTimesheetOfWeekByCategory(int categoryId, DateTime date)
        {
            var dateRange = DateHelper.GetWeekRangeOfCurrentDate(date);

            return this.repository.FindAll(
                m => m.ConsumptionId == categoryId && m.Date >= dateRange.Item1 && m.Date <= dateRange.Item2);
        }

        protected override ConsumptionStatistic FindSpecificDataOfDateTime(int categoryId, DateTime date)
        {
            return this.repository.FindAll(m => m.ConsumptionId == categoryId && m.Date == date).FirstOrDefault();
        }

        protected override void UpdateDataOfTime(ConsumptionStatistic s)
        {
            this.repository.Update(s);
        }

        protected override void AddNewDataForTime(int categoryId, double value, DateTime date)
        {
            ConsumptionStatistic c = new ConsumptionStatistic
            {
                ConsumptionId = categoryId,
                Value = value,
                Date = date,
            };

            this.repository.Add(c);
        }
    }
}
