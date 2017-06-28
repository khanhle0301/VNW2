using MyShop.Common.Exceptions;
using MyShop.Data.Infrastructure;
using MyShop.Data.Repositories;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface ICapBacService
    {
        CapBac Add(CapBac capBac);

        void Update(CapBac capBac);

        void Delete(int id);

        IEnumerable<CapBac> GetAll();

        IEnumerable<CapBac> GetAll(string keyword);

        CapBac GetById(int id);

        void Save();
    }

    public class CapBacService : ICapBacService
    {
        private ICapBacRepository _capBacRepository;
        private IUnitOfWork _unitOfWork;

        public CapBacService(ICapBacRepository capBacRepository, IUnitOfWork unitOfWork)
        {
            _capBacRepository = capBacRepository;
            _unitOfWork = unitOfWork;
        }

        public CapBac Add(CapBac capBac)
        {
            if (_capBacRepository.CheckContains(x => x.Ten == capBac.Ten))
                throw new NameDuplicatedException("Tên không được trùng");
            return _capBacRepository.Add(capBac);
        }

        public void Delete(int id)
        {
            var capBac = _capBacRepository.GetSingleById(id);
            capBac.Status = false;
            _capBacRepository.Update(capBac);
        }

        public IEnumerable<CapBac> GetAll()
        {
            return _capBacRepository.GetMulti(x => x.Status == true);
        }

        public IEnumerable<CapBac> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _capBacRepository.GetMulti(x => x.Status == true & x.Ten.Contains(keyword));
            else
                return GetAll();
        }

        public CapBac GetById(int id)
        {
            return _capBacRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(CapBac capBac)
        {
            if (_capBacRepository.CheckContains(x => x.Ten == capBac.Ten && x.Id != capBac.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _capBacRepository.Update(capBac);
        }
    }
}