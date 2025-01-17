﻿using NETAPI_SEM3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETAPI_SEM3.Services
{
    public class AdsPackageServiceImpl : AdsPackageService
    {

        private readonly ProjectSem3DBContext _db;

        public AdsPackageServiceImpl(ProjectSem3DBContext db)
        {
            this._db = db;
        }

        public AdPackage GetAdPackageByid(int id)
        {
            try
            {
                return _db.AdPackages.Find(id);

            }
            catch
            {
                return null;
            }

        }

        public bool DeleteAdsPackage(int id)
        {
            try
            {
                var adsPackage = _db.AdPackages.Find(id);
                adsPackage.IsDelete = true;
                _db.AdPackages.Update(adsPackage);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetAllAdsPackage()
        {
            return _db.AdPackages.Where(a => a.IsDelete == false).Count();
        }

        public IEnumerable<AdPackage> GetAllAdsPackagePage(int page)
        {
            var start = 10 * (page - 1);
            var adPackage = _db.AdPackages.Where(a => a.IsDelete == false).ToList();
            return adPackage.Skip(start).Take(10).ToList();
        }

        public bool UpdateStatus(int id, bool status)
        {
            try
            {
                var adsPackage = _db.AdPackages.Find(id);
                adsPackage.StatusBuy = status;
                _db.AdPackages.Update(adsPackage);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetAllAdsPackageForSalePage()
        {
            return _db.AdPackages.Where(a => a.IsDelete == false && a.StatusBuy == true).Count();
        }

        public IEnumerable<AdPackage> GetAllAdsPackageForSalePagePerPage(int page)
        {
            var start = 10 * (page - 1);
            var adPackage = _db.AdPackages.Where(a => a.IsDelete == false && a.StatusBuy == true).ToList();
            return adPackage.Skip(start).Take(10).ToList();
        }

        public int SearchAdsPackage(string status, string name, string price)
        {
            IEnumerable<AdPackage> adPackages = null;
            if (status.Equals("true"))
            {
                adPackages = _db.AdPackages.Where(a => a.IsDelete == false && a.StatusBuy == bool.Parse(status)).ToList();
            }
            else
            {
                adPackages = _db.AdPackages.Where(a => a.IsDelete == false).ToList();
            }
            if (!name.Equals(".all"))
            {
                adPackages = adPackages.Where(a => a.NameAdPackage.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!price.Equals(".all"))
            {
                adPackages = adPackages.Where(a => a.Price <= Convert.ToDecimal(price)).ToList();
            }
            return adPackages.Count();
        }

        public IEnumerable<AdPackage> SearchAdsPackagePage(string status, string name, string price, int page)
        {
            IEnumerable<AdPackage> adPackages = null;
            if (status.Equals("true"))
            {
                adPackages = _db.AdPackages.Where(a => a.IsDelete == false && a.StatusBuy == bool.Parse(status)).ToList();
            }
            else
            {
                adPackages = _db.AdPackages.Where(a => a.IsDelete == false).ToList();
            }
            if (!name.Equals(".all"))
            {
                adPackages = adPackages.Where(a => a.NameAdPackage.ToLower().Contains(name.ToLower())).ToList();
            }
            if (!price.Equals(".all"))
            {
                adPackages = adPackages.Where(a => a.Price <= Convert.ToDecimal(price)).ToList();
            }
            return adPackages;
        }

        public double GetMaxPrice()
        {
            return (double)_db.AdPackages.Where(a => a.IsDelete == false).Select(a => a.Price).Max();
        }

        public bool UpdateAdsPackage(AdPackage adPackage)
        {
            try
            {
                _db.AdPackages.Update(adPackage);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public AdPackage CreateAdsPackage(AdPackage adPackage)
        {
            try
            {
                if (adPackage != null)
                {
                    _db.AdPackages.Add(adPackage);
                    _db.SaveChanges();
                }
                return adPackage;
            }
            catch
            {
                return null;
            }
        }

        public MemberPackageDetail CreateMemberPackageDetail(MemberPackageDetail memberPackageDetail)
        {
            try
            {
                if (memberPackageDetail != null)
                {
                    _db.MemberPackageDetails.Add(memberPackageDetail);
                    _db.SaveChanges();
                }
                return memberPackageDetail;
            }
            catch
            {
                return null;
            }
        }

        public bool DeletePackageDetail(int memberId)
        {
            try
            {
                var packageDetail = _db.MemberPackageDetails.SingleOrDefault(pd => pd.MemberId == memberId);
                _db.Remove(packageDetail);
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetPeriodDay(int id)
        {
            return _db.AdPackages.FirstOrDefault(a => a.PackageId == id).Period;
        }

        public int GetPostLimit(int packageId)
        {
            var postNumber = 0;
            if (packageId > 0)
            {
                postNumber = _db.AdPackages.FirstOrDefault(a => a.PackageId == packageId).PostNumber;
            }
            return postNumber;
        }

        public int GetPackageIdByMemberId(int memberId)
        {
            var packageDetail = _db.MemberPackageDetails.SingleOrDefault(pd => pd.MemberId == memberId);
            var packageId = 0;
            if (packageDetail != null)
            {
                packageId = packageDetail.PackageId;
            }
            return packageId;
        }

        public bool CheckExpiryDate(int memberId)
        {
            var packageDetail = _db.MemberPackageDetails.SingleOrDefault(pd => pd.MemberId == memberId);
            var result = true;

            if (packageDetail != null)
            {
                var expiryDate = packageDetail.ExpiryDate;
                var today = DateTime.Now;

                if (today.CompareTo(expiryDate) > 0)
                {
                    result = false;
                }
                else if (today.CompareTo(expiryDate) <= 0)
                {
                    result = true;
                }
            }
            return result;
        }

        public int CheckPackage(int memberId)
        {
            try
            {
                return _db.MemberPackageDetails.Count(pd => pd.MemberId == memberId);
            }
            catch
            {
                return 0;
            }
        }
    }
}
