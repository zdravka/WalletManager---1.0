using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WalletManager.DataAccess.Interface;
using WalletManager.Models;

namespace WalletManager.DataAccess.Source
{
    public class FundsTypeRepository : IFundsTypeRepository
    {
         Context _context;

        public FundsTypeRepository(Context context)
		{
			this._context = context;
		}

        public IEnumerable<Models.FundsTypeModel> GetFundsTypes()
        {
            return _context.FundsType.ToList();
        }

        public Models.FundsTypeModel GetFundsTypeByID(int mfId)
        {
            return _context.FundsType.Find(mfId);
        }

        public void InsertFundsTypes(Models.FundsTypeModel mf)
        {
            _context.FundsType.Add(mf);
        }

        public void DeleteFundsTypes(int mfId)
        {
            FundsTypeModel type = _context.FundsType.Find(mfId);
            _context.FundsType.Remove(type);
        }

        public void UpdateFundsTypes(Models.FundsTypeModel mf)
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