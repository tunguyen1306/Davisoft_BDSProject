using System;
using System.Transactions;

namespace Davisoft_BDSProject.Domain
{
    /// Start a transaction, retries for 3 times beofore actual throw an exception if error orcur, otherwise, comCPO.
    /// Usage:
    /// TransactionWrapper.Do(() =>
    /// {
    ///     your code here...
    /// });
    public class TransactionWrapper
    {
        public static void Do(Action work)
        {
            using (var scope = new TransactionScope())
            {
                int retries = 0;
                bool succeeded = false;

                while (!succeeded)
                {
                    try
                    {
                        work();
                        scope.Complete();
                        succeeded = true;
                    }
                    catch (Exception)
                    {
                        if (retries > 0)
                            retries--;
                        else
                            throw;
                    }
                }
            }
        }

        public static T Do<T>(Func<T> work)
        {
            T result = default(T);

            using (var scope = new TransactionScope())
            {
                int retries = 0;
                bool succeeded = false;

                while (!succeeded)
                {
                    try
                    {
                        bool returnresult = true;
                        result = work();
                        try
                        {
                            returnresult = Convert.ToBoolean(result);
                        }
                        catch { }
                        if (returnresult)
                        {
                            scope.Complete();
                        }
                        succeeded = true;
                    }
                    catch (Exception)
                    {
                        if (retries > 0)
                            retries--;
                        else
                            throw;
                    }
                }
            }
            return result;
        }
    }
}
