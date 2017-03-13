using System;
using Davisoft_BDSProject.Domain.Abstract;
using Davisoft_BDSProject.Domain.Entities;
using Davisoft_BDSProject.Web.Infrastructure.Utility;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public static class MoneyHelper
    {
        private const string ERROR = "-";
        private static Currency _baseCurrency;

        public static Currency DefaultCurrency
        {
            get
            {
                if (_baseCurrency == null)
                {
                    var repo = DependencyHelper.GetService<IUnitRepository>();
                    _baseCurrency = repo.GetDefaultCurrency();
                }

                return _baseCurrency;
            }
        }
        public static void SetDefaultCurrency()
        {
            var repo = DependencyHelper.GetService<IUnitRepository>();
            _baseCurrency = repo.GetDefaultCurrency();
        }

        public static string ToMoneyString(this decimal money)
        {
            string format = "N" + DefaultCurrency.Precision;
            //return money.ToString(DefaultCurrency.Symbol + " ##,##0.00");
            return DefaultCurrency.Symbol + " " + money.ToString(format);
        }
        public static string ToMoneyStringWithNoSymbol(this decimal money)
        {
            string format = "N" + DefaultCurrency.Precision;
            //return money.ToString(DefaultCurrency.Symbol + " ##,##0.00");
            return money.ToString(format);
        }
        public static string ToMoneyString(this decimal money, Currency currency)
        {
            //return money.ToString(currency.Symbol + " ##,##0.00");
            string format = "N" + currency.Precision;
            return currency.Symbol + " " + money.ToString(format);
        }

        public static decimal DecimalFormat(this decimal money)
        {
            return decimal.Round(money, 2, MidpointRounding.AwayFromZero);
        }
        public static string AsNumber(this int value)
        {
            return value.ToString("N0");
        }

        public static string AsMoney(this decimal value, Currency currency, bool withUnit = false)
        {
            if (currency == null)
                throw new ArgumentNullException("currency", "Currency cannot be null.");

            return currency.Format(value, withUnit);
        }
    }
}
