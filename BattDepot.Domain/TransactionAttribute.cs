using System;
using System.Transactions;
using PostSharp.Aspects;

namespace Davisoft_BDSProject.Domain
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : MethodInterceptionAspect
    {
        private int _retries = 3;

        public TransactionAttribute(int retries = 3)
        {
            _retries = retries;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            using (var scope = new TransactionScope())
            {
                bool succeeded = false;

                while (!succeeded)
                {
                    try
                    {
                        args.Proceed();
                        scope.Complete();
                        succeeded = true;
                    }
                    catch (Exception)
                    {
                        if (_retries > 0)
                            _retries--;
                        else
                            throw;
                    }
                }
            }
        }
    }
}
