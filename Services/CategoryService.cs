using NETAPI_SEM3.Entities;
using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Services
{
    public interface CategoryService
    {
        public List<Category> GetAllCategory();

        public int createCategory(Category category);

        public bool updateCategory(Category category);
        public bool deleteCategory(int categoryId);
        public Category findCategory(int categoryId);
        public List<NewProperty> PropertyByCategory(int categoryId);
    }
}
