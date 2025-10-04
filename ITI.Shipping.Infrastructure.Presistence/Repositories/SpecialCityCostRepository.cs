﻿using ITI.Shipping.Core.Domin.Entities;
using ITI.Shipping.Core.Domin.Repositories.contract;
using ITI.Shipping.Infrastructure.Presistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ITI.Shipping.Infrastructure.Presistence.Repositories;
// This Is A SpecialCityCost Repository Class That Implements The ISpecialCityCostRepository Interface
public class SpecialCityCostService:GenericRepository<SpecialCityCost,int>, ISpecialCityCostRepository
{
    private readonly ApplicationContext _applicationContext;
    public SpecialCityCostService(ApplicationContext applicationContext) : base(applicationContext)
    {
        _applicationContext = applicationContext;
    }
    public async Task AddRangeAsync(IEnumerable<SpecialCityCost> entities)
    {
        await _applicationContext.SpecialCityCost.AddRangeAsync(entities);
        await _applicationContext.SaveChangesAsync();
    }

    public async Task<SpecialCityCost> GetCityCostByMarchantId(string MerchantId ,int CityId)
    {
      var CityCostByMarchantId = await _applicationContext.SpecialCityCost.FirstOrDefaultAsync
            (x=>x.MerchantId == MerchantId && x.CitySettingId == CityId);
        if(CityCostByMarchantId == null)
            return null!;
        return CityCostByMarchantId;
    }

    public async Task<IEnumerable<SpecialCityCost>> GetSpecialCityOfMerchant(string MerchatId)
    {
        var SpecialCityOfMerchant = await _applicationContext.SpecialCityCost.
             Where(x => x.MerchantId == MerchatId).ToListAsync();
        if(SpecialCityOfMerchant ==null)
            return Enumerable.Empty<SpecialCityCost>();
        return SpecialCityOfMerchant;
    }
}