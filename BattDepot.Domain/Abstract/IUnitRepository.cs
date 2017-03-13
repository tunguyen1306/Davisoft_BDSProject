using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using Davisoft_BDSProject.Domain.Entities;

namespace Davisoft_BDSProject.Domain.Abstract
{
    public interface IUnitRepository
    {

        #region Currency
        IEnumerable<Currency> GetAllCurrencies();
        Currency GetCurrency(int currencyID);
        Currency GetCurrency(string name);
        Currency CreateCurrency(Currency info);
        Currency QuickCreateCurrency(string name);
        bool UpdateCurrency(Currency info);
        bool DeleteCurrency(int currencyID);
        Currency GetDefaultCurrency();
        void SetDefaultCurrency(int currencyID);
        #endregion Currency

        #region Branch
        IEnumerable<Branch> GetAllBranches();
        Branch GetBranch(int brandID);
        Branch GetBranchByCode(string brandCode);
        Branch GetBranchByName(string name);
        Branch CreateBranch(Branch brand);
        bool UpdateBranch(Branch brand);
        bool DeleteBranch(int brandID);
        void AddToUserBranchList(int userID, int branchID);
        IEnumerable<Branch> GetAllBranchsofSalesman();
        #endregion Branch

        #region Counter
        int GetCounter(string name);
        Counter SetCounter(string name, int value);
        IEnumerable<Counter> GetAllCounter();
        decimal GetDepositNextCounter(string name, int start);

        #endregion Counter

        #region Holiday
        IEnumerable<Holiday> GetAllHolidays();
        IEnumerable<Holiday> GetAllHolidays(int year);
        Holiday GetHoliday(int holidayID);
        Holiday GetHoliday(DateTime holidayDate);
        Holiday CreateHoliday(Holiday holiday);
        bool UpdateHoliday(Holiday holiday);
        bool DeleteHoliday(DateTime date);
        IEnumerable<Holiday> GetAllHolidaysbyMonthAndYear(int month, int year);
        #endregion Holiday

        #region Email Template setting

        EmailReceiveSetting GetEmailReceiveSetting(string type, string nameTemplate);
        EmailReceiveSetting GetEmailReceiveSetting(int emailReceiveID);
        bool CreateEmailReceive(EmailReceiveSetting emailReceive);
        bool DeleteEmailAllInTemplate(string type, string nameTemplate);

        #endregion

        #region Language
        IEnumerable<Language> GetAllLanguages();
        Language GetLanguage(string value);
        Language GetLanguage(int id);
        Language CreateLanguage(Language language);
        bool UpdateLanguage(Language language);
        bool DeleteLanguage(int languageId);
        #endregion Language

    }
}
