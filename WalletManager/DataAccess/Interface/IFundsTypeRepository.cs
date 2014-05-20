using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletManager.Models;

namespace WalletManager.DataAccess.Interface
{
    public interface IFundsTypeRepository : IDisposable
    {
        IEnumerable<FundsTypeModel> GetFundsTypes();
        FundsTypeModel GetFundsTypeByID(int mfId);
        void InsertFundsTypes(FundsTypeModel mf);
        void DeleteFundsTypes(int mfId);
        void UpdateFundsTypes(FundsTypeModel mf);
        void Save();
    }
}
