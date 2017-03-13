using FluentValidation;
using CPO.Domain.Abstract;
using CPO.Domain.Entities;
using Resources;

namespace CPO.Web.Validation
{
    public class GenerateChequeValidator: AbstractValidator<GenerateCheque>
    {
        private static IInvoiceService _repo;

        public GenerateChequeValidator(IInvoiceService repo)
        {
            _repo = repo;

            RuleFor(m => m.GenerateNo).NotEqual(0).WithMessage(Resource.TheFieldNotEqualTo0);
            RuleFor(m => m.GenerateNo).Must(CheckExist).WithMessage(Resource.ChequeNoAlreadyExist);
        }
        private bool CheckExist(GenerateCheque cheque, int generateNo)
        {
            var generateCheque = _repo.GetGenerateChequeByNo(generateNo);
            return generateCheque == null || generateCheque.ID == cheque.ID;
        }

    }
}
