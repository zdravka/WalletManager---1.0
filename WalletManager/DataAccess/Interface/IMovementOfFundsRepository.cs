using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletManager.Models;

namespace WalletManager.DataAccess.Interface
{
    public interface IMovementOfFundsRepository : IDisposable
    {
        IEnumerable<MovementOfFundsModel> GetMovementOfFunds();
        MovementOfFundsModel GetMovementOfFundsByID(int mfId);
        void InsertMovementOfFunds(MovementOfFundsModel mf);
        void DeleteMovementOfFunds(int mfId);
        void UpdateMovementOfFunds(MovementOfFundsModel mf);
        void Save();
    }
}
