using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WalletManager.DataAccess.Interface;
using WalletManager.Models;

namespace WalletManager.DataAccess.Source
{
    public class MovementOfFundsRepository : IMovementOfFundsRepository
    {
        Context _context;

        public MovementOfFundsRepository(Context context)
		{
			this._context = context;
		}



        public IEnumerable<Models.MovementOfFundsModel> GetMovementOfFunds()
        {
            return _context.MovementOfFunds.ToList();
        }

        public Models.MovementOfFundsModel GetMovementOfFundsByID(int mfId)
        {
            return _context.MovementOfFunds.Find(mfId);
        }

        public void InsertMovementOfFunds(Models.MovementOfFundsModel mf)
        {
            _context.MovementOfFunds.Add(mf);
        }

        public void DeleteMovementOfFunds(int mfId)
        {
            MovementOfFundsModel movement = _context.MovementOfFunds.Find(mfId);
            _context.MovementOfFunds.Remove(movement);
        }

        public void UpdateMovementOfFunds(Models.MovementOfFundsModel mf)
        {
            _context.Entry(mf).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}