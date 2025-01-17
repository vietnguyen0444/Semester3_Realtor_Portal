﻿using NETAPI_SEM3.Entities;
using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Services.User
{
    public interface IndexService 
    {
        public List<NewProperty> LoadTopProperty() ;
        
        public List<NewCountry> LoadCountries();
        public List<NewCategory> LoadCategories();
        public List<PopularLocations> LoadPopularLocations(); 

    }
}
