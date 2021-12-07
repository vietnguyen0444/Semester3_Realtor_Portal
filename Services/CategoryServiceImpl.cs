using NETAPI_SEM3.Entities;
using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Services
{
	public class CategoryServiceImpl : CategoryService
	{
		private readonly ProjectSem3DBContext db;

		public CategoryServiceImpl(ProjectSem3DBContext _db)
		{
			this.db = _db;
		}

		public List<Category> GetAllCategory()
		{
			return db.Categories.Select(c=> new Category
			{ 
				CategoryId = c.CategoryId,
				IsShow = c.IsShow,
				Name = c.Name
			}).Where(category => category.IsShow == true).ToList();
		}

		public int createCategory(Category category)
		{
			try
			{
				db.Categories.Add(category);
				db.SaveChanges();
				var lastId = db.Categories.Max(category => category.CategoryId);
				return lastId;
			}
			catch
			{
				return 0;
			}
		}

		public bool updateCategory(Category category)
		{
			try
			{
				var oldCategory = db.Categories.Find(category.CategoryId);
				if (oldCategory != null)
				{
					oldCategory.Name = category.Name;
					db.SaveChanges();
					return true;
				}
				else
				{
					return false;
				}
			}
			catch
			{
				return false;
			}
		}

		public bool deleteCategory(int categoryId)
		{
			try
			{
				var category = db.Categories.Find(categoryId);
				category.IsShow = false;
				db.SaveChanges();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public Category findCategory(int categoryId)
		{
			return db.Categories.Find(categoryId);
		}

		public List<NewProperty> PropertyByCategory(int categoryId)
		{
			return db.Properties.Where(p => p.CategoryId == categoryId).Select(
				p => new NewProperty
				{
					PropertyId = p.PropertyId,
					Address = p.Address,
					Area = p.Area,
					BedNumber = p.BedNumber,
					CategoryId = p.CategoryId,
					CategoryName = p.Category.Name,
					CityId = p.CityId,
					CityName = p.City.Name,
					Description = p.Description,
					MemberId = p.MemberId,
					MemberName = p.Member.FullName,
					MemberType = db.Roles.SingleOrDefault(r => r.Id.Equals(p.Member.RoleId)).Name,
					Price = (double)p.Price,
					RoomNumber = p.RoomNumber,
					SoldDate = p.SoldDate,
					UploadDate = p.UploadDate,
					StatusId = p.StatusId,
					StatusName = p.Status.Name,
					Title = p.Title,
					Type = p.Type,

					Images = p.Images.Select(i => new NewImageProperty
					{
						ImageId = i.ImageId,
						Name = i.Name,
						PropertyId = i.PropertyId ?? default(int)
					}).ToList()
				}).ToList();
		}

	}
}
