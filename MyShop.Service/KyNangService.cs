using MyShop.Common.Exceptions;
using MyShop.Data.Infrastructure;
using MyShop.Data.Repositories;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface IKyNangService
    {
        KyNang Add(KyNang kyNang);

        void Update(KyNang kyNang);

        void Delete(int id);

        IEnumerable<KyNang> GetAll();

        IEnumerable<KyNang> GetAll(string keyword);

        KyNang GetById(int id);

        void Save();
    }

    internal class KyNangService : IKyNangService
    {
        private IKyNangRepository _kyNangRepository;
        private IUnitOfWork _unitOfWork;

        public KyNangService(IKyNangRepository kyNangRepository, IUnitOfWork unitOfWork)
        {
            _kyNangRepository = kyNangRepository;
            _unitOfWork = unitOfWork;
        }

        public KyNang Add(KyNang kyNang)
        {
            if (_kyNangRepository.CheckContains(x => x.Ten == kyNang.Ten))
                throw new NameDuplicatedException("Tên không được trùng");
            return _kyNangRepository.Add(kyNang);
        }

        public void Delete(int id)
        {
            var kyNang = _kyNangRepository.GetSingleById(id);
            kyNang.Status = false;
            _kyNangRepository.Update(kyNang);
        }

        public IEnumerable<KyNang> GetAll()
        {
            return _kyNangRepository.GetMulti(x => x.Status == true);
        }

        public IEnumerable<KyNang> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _kyNangRepository.GetMulti(x => x.Status == true & x.Ten.Contains(keyword));
            else
                return GetAll();
        }

        public KyNang GetById(int id)
        {
            return _kyNangRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(KyNang kyNang)
        {
            if (_kyNangRepository.CheckContains(x => x.Ten == kyNang.Ten && x.Id != kyNang.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _kyNangRepository.Update(kyNang);
        }
    }
}