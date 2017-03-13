using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Windows.Forms.VisualStyles;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Humanizer;
using NS.Entity;
using ServiceStack;
using WebGrease.Css;

namespace Davisoft_BDSProject.Domain.Concrete
{
    public class EFUnitRepository : Repository, IUnitRepository
    {
        public EFUnitRepository(DbContext db)
            : base(db)
        {
        }

        #region Currencies

        public IEnumerable<Currency> GetAllCurrencies()
        {
            return GetAll<Currency>().AsEnumerable();
        }

        public Currency GetCurrency(int currencyID)
        {
            return Get<Currency>(currencyID);
        }

        public Currency GetCurrency(string name)
        {
            return Get<Currency>(m => String.Equals(m.Name.Trim(), name.Trim(), StringComparison.CurrentCultureIgnoreCase));
        }

        public Currency CreateCurrency(Currency info)
        {
            Currency currency = Create(info);

            if (currency.IsDefault)
                SetDefaultCurrency(currency.ID);

            return currency;
        }

        public Currency QuickCreateCurrency(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            var currency = Get<Currency>(c => c.Name != null && c.Name.ToUpper() == name.ToUpper());
            if (currency == null)
            {
                currency = new Currency { Name = name, Symbol = "$", IsDefault = false };
                currency = Create(currency);
            }

            return currency;
        }

        public bool UpdateCurrency(Currency info)
        {
            bool result = Update(info);

            if (info.IsDefault)
                SetDefaultCurrency(info.ID);

            return result;
        }

        public bool DeleteCurrency(int currencyID)
        {
            return Delete<Currency>(currencyID);
        }

        public Currency GetDefaultCurrency()
        {
            return Get<Currency>(c => c.IsDefault) ??
                   Get<Currency>(c => true);
        }

        public void SetDefaultCurrency(int currencyID)
        {
            Currency currency = GetCurrency(currencyID);
            if (currency != null)
            {
                Currency baseCurrency = GetDefaultCurrency();
                if (baseCurrency != null && baseCurrency.ID != currencyID)
                {
                    baseCurrency.IsDefault = false;
                    UpdateCurrency(baseCurrency);

                    currency.IsDefault = true;
                    UpdateCurrency(currency);
                }
            }
        }
        #endregion

        #region Branch

        public IEnumerable<Branch> GetAllBranches()
        {
            return GetAll<Branch>(m => m.EntityStatus == EntityStatus.Normal).ToList();
        }

        public Branch GetBranch(int brandID)
        {
            return Get<Branch>(brandID);
        }

        public Branch GetBranchByCode(string brandCode)
        {
            if (brandCode == null)
                return null;
            return Get<Branch>(m => m.Code.ToLower().Trim() == brandCode.ToLower().Trim());
        }
        public Branch GetBranchByName(string name)
        {
            if (name == null)
                return null;
            return Get<Branch>(m => m.Name.ToLower().Trim() == name.ToLower().Trim());
        }

        public Branch CreateBranch(Branch brand)
        {
            //brand.BranchType = BranchType.Salesman;
            return Create(brand);
        }

        public bool UpdateBranch(Branch brand)
        {
            return Update(brand);
        }

        public bool DeleteBranch(int brandID)
        {
            return Delete<Branch>(brandID);
        }

        public void AddToUserBranchList(int userID, int branchID)
        {
            var user = Get<User>(userID);
            if (user == null) return;

            var branches = user.Branches.ToList();
            if (!branches.Select(m => m.ID).Contains(branchID))
            {
                branches.Add(Get<Branch>(branchID));
                user.Branches.Clear();
                user.Branches = branches;

                _db.SaveChanges();
            }
        }

        #endregion

        #region Counter

        public int GetCounter(string name)
        {
            return Get<Counter>(c => c.Name == name).Value;
        }

        public Counter SetCounter(string name, int value)
        {
            Counter counter = new Counter()
                              {
                                  Name = name,
                                  Value = value
                              };
            return Create(counter);
        }

        public IEnumerable<Counter> GetAllCounter()
        {
            return GetAll<Counter>();
        }

        #endregion

        #region Holiday

        public IEnumerable<Holiday> GetAllHolidays()
        {
            return GetAll<Holiday>().ToList();
        }

        public IEnumerable<Holiday> GetAllHolidays(int year)
        {
            return GetAll<Holiday>(m => m.Date.Year == year).ToList();
        }

        public Holiday GetHoliday(int holidayID)
        {
            return Get<Holiday>(holidayID);
        }

        public Holiday GetHoliday(DateTime holidayDate)
        {
            return Get<Holiday>(m => m.Date.Date == holidayDate.Date);
        }

        public Holiday CreateHoliday(Holiday holiday)
        {
            Holiday holidayExist = GetHoliday(holiday.Date);
            if (holidayExist != null)
            {
                holidayExist.Description = holiday.Description;
                holidayExist.IsFullDay = holiday.IsFullDay;
                Update(holidayExist);
                return holidayExist;
            }
            return Create(holiday);
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            return Update(holiday);
        }

        public bool DeleteHoliday(DateTime date)
        {
            Holiday holidayExist = GetHoliday(date);
            if (holidayExist != null)
                return Delete<Holiday>(holidayExist.ID);
            return false;
        }

        public IEnumerable<Holiday> GetAllHolidaysbyMonthAndYear(int month, int year)
        {
            return GetAll<Holiday>(m => m.Date.Month == month && m.Date.Year == year);
        }

        #endregion

        #region Email Template Settings

        //[RequestCache]
        public EmailReceiveSetting GetEmailReceiveSetting(string type, string nameTemplate)
        {
            if (string.IsNullOrEmpty(nameTemplate) || string.IsNullOrEmpty(type))
            {
                return null;
            }
            return Single<EmailReceiveSetting>(e => e.TemplateName.ToUpper().Trim() == nameTemplate.ToUpper().Trim() && e.Type.ToUpper().Trim() == type.ToUpper().Trim());
        }
        public EmailReceiveSetting GetEmailReceiveSetting(int emailReceiveID)
        {
            return Single<EmailReceiveSetting>(m => m.ID == emailReceiveID);
        }
        public bool CreateEmailReceive(EmailReceiveSetting emailReceive)
        {
            if (emailReceive.ID > 0)
            {
                var emailExist = Single<EmailReceiveSetting>(m => m.ID == emailReceive.ID);

                emailExist.ReceiveRoles = emailReceive.ReceiveRoles;
                emailExist.ReceiveUsers = emailReceive.ReceiveUsers;
                UpdateCollection(emailReceive, e => e.ID == emailReceive.ID, e => e.ReceiveRoles, r => r.ID);
                UpdateCollection(emailReceive, e => e.ID == emailReceive.ID, e => e.ReceiveUsers, u => u.ID);
                return true;
            }
            return Create(emailReceive) != null;
        }

        public bool DeleteEmailAllInTemplate(string type, string nameTemplate)
        {
            var list = GetAll<EmailReceiveSetting>().ToList().Where(e => e.TemplateName.ToUpper().Trim() == nameTemplate.ToUpper().Trim() && e.Type.ToUpper().Trim() == type.ToUpper().Trim()).ToList();
            foreach (var emailReceiveSetting in list)
            {
                Delete<EmailReceiveSetting>(emailReceiveSetting.ID);
            }
            return true;
        }

        public IEnumerable<Branch> GetAllBranchsofSalesman()
        {
            return GetAll<Branch>();
        }

        public decimal GetDepositNextCounter(string name, int start)
        {
            return GetNextCounter(name, start);
        }

        #endregion

        #region Language Setting

        public IEnumerable<Language> GetAllLanguages()
        {
            return GetAll<Language>().ToList();
        }

        public Language GetLanguage(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            return
                Get<Language>(
                    m => m.Value.ToLower().Contains(value.ToLower()) || value.ToLower().Contains(m.Value.ToLower()));
        }
        public Language GetLanguage(int id)
        {
            return Single<Language>(m => m.ID == id);
        }

        public Language CreateLanguage(Language language)
        {
            language.Value = language.Value.Trim().ToLower();
            var cs = new CultureInfo(language.Value);
            var r = new RegionInfo(cs.LCID);
            language.Image = "flag-icon flag-icon-" + r.TwoLetterISORegionName.ToLower();
            language.DisplayName = cs.Parent.EnglishName;
            return Create(language);
        }

        public bool UpdateLanguage(Language language)
        {
            return Update(language);
        }

        public bool DeleteLanguage(int languageId)
        {
            return Delete<Language>(languageId);
        }
        #endregion
    }
}
