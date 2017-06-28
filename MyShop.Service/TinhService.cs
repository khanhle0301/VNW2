using MyShop.Data.Infrastructure;
using MyShop.Data.Repositories;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface ITinhService
    {
        IEnumerable<Tinh> GetAll();
    }

    public class TinhService : ITinhService
    {
        private ITinhRepository _tinhRepository;
        private IUnitOfWork _unitOfWork;

        public TinhService(ITinhRepository tinhRepository, IUnitOfWork unitOfWork)
        {
            this._tinhRepository = tinhRepository;
            this._unitOfWork = unitOfWork;
        }

        public IEnumerable<Tinh> GetAll()
        {
            return _tinhRepository.GetAll();
        }
    }
}