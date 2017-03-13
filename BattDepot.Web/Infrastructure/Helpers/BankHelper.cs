using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Davisoft_BDSProject.Web.Infrastructure.Helpers
{
    public class BankHelper
    {
        public static BankCheque GetBankOfCheque(string chequeNo)
        {
            if (chequeNo == null)
            {
                return new BankCheque();
            }
            chequeNo = chequeNo.Trim();
            int flag = -1;
            var str = "0123456789";
            for (int i = 0; i < chequeNo.Length; i++)
            {
                if (str.Contains(chequeNo[i]))
                {
                    if (flag == -1)
                        flag = i;
                }
                else
                    flag = -1;
            }
            var obj = new BankCheque
                      {
                          Bank = chequeNo.Substring(0, flag),
                          ChequeNo = Convert.ToInt32(chequeNo.Substring(flag, chequeNo.Length - flag))
                      };
            return obj;
        }
    }

    public class BankCheque
    {
        public string Bank { get; set; }
        public int ChequeNo { get; set; }
    }
}