﻿using Microsoft.EntityFrameworkCore;
using NETAPI_SEM3.Models;
using NETAPI_SEM3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Diagnostics;

namespace NETAPI_SEM3.Services
{
    public class AddressServiceImpl : AddressService
    {
        private readonly ProjectSem3DBContext _db;


        public AddressServiceImpl(ProjectSem3DBContext db

            )
        {
            this._db = db;

        }

        public IEnumerable<Region> GetAllRegion()
        {
            return _db.Regions.Select(r=> new Region{
                RegionId = r.RegionId,
                Name = r.Name
            }).ToList();
        }

        public IEnumerable<CountryViewModel> GetAllCountry(int regionId)
        {
            return _db.Countries.Where(c => c.RegionId == regionId).Select(c => new CountryViewModel
            {
                RegionId = c.RegionId,
                CountryId = c.CountryId,
                Name = c.Name,
            }).ToList();
        }

        public IEnumerable<CityViewModel> GetAllCity(int countryId)
        {
            return _db.Cities.Where(c => c.CountryId == countryId).Select(c => new CityViewModel
            {
                CityId = c.CityId,
                Name = c.Name,
                CountryId = c.CountryId
            }).ToList();
            //return _db.Cities.Where(c => c.CountryId == countryId).ToList();
        }
    }
}
