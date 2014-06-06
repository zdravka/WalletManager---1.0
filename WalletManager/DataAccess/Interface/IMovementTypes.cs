using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletManager.Models;

namespace WalletManager.DataAccess.Interface
{
    public interface IMovementTypes : IDisposable
    {
        IEnumerable<MovementTypesModel> GetMovementTypes();
        MovementTypesModel GetMovementOfFundsByID(int mtId);
        void InsertMovementTypes(MovementTypesModel mt);
        void DeleteMovementTypes(int mtId);
        void UpdateMovementTypes(MovementTypesModel mt);
        void Save();
    }
}
