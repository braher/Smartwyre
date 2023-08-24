using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data
{
    public  interface IRebateRepository
    {
        IEnumerable<Rebate> GetAllRebates();

        Rebate GetRebateById(int rebateId);

        Rebate GetRebateByIdentifier(string rebateIdentifier);

        void AddRebate(Rebate rebate);

        void UpdateRebate(Rebate rebate);

        void DeleteRebate(int rebateId);

        void Save();

        void StoreCalculationResult(Rebate account, decimal rebateAmount);
    }
}
