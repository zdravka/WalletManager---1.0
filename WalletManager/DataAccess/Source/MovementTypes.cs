using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WalletManager.DataAccess.Interface;
using WalletManager.Models;

namespace WalletManager.DataAccess.Source
{
    public class MovementTypes : IMovementTypes
    {
        Context _context;
        public MovementTypes(Context context)
        {
            this._context = context;
        }
        public IEnumerable<Models.MovementTypesModel> GetMovementTypes()
        {
            return _context.MOvementType.ToList();
        }

        public Models.MovementTypesModel GetMovementOfFundsByID(int mtId)
        {
            return _context.MOvementType.Find(mtId);
        }

        public void InsertMovementTypes(Models.MovementTypesModel mt)
        {
            _context.MOvementType.Add(mt);
        }

        public void DeleteMovementTypes(int mtId)
        {
            MovementTypesModel movement = _context.MOvementType.Find(mtId);
            _context.MOvementType.Remove(movement);
        }

        public void UpdateMovementTypes(Models.MovementTypesModel mt)
        {
            _context.Entry(mt).State = EntityState.Modified;
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