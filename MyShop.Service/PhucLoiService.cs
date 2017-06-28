using MyShop.Common.Exceptions;
using MyShop.Data.Infrastructure;
using MyShop.Data.Repositories;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface IPhucLoiService
    {
        PhucLoi Add(PhucLoi phucLoi);

        void Update(PhucLoi phucLoi);

        void Delete(int id);

        IEnumerable<PhucLoi> GetAll();

        IEnumerable<PhucLoi> GetAll(string keyword);

        PhucLoi GetById(int id);

        void Save();
    }
    class PhucLoiService : IPhucLoiService
    {
        private IPhucLoiRepository _phucLoiRepository;
        private IUnitOfWork _unitOfWork;

        public PhucLoiService(IPhucLoiRepository phucLoiRepository, IUnitOfWork unitOfWork)
        {
            _phucLoiRepository = phucLoiRepository;
            _unitOfWork = unitOfWork;
        }

        public PhucLoi Add(PhucLoi phucLoi)
        {
            if (_phucLoiRepository.CheckContains(x => x.Ten == phucLoi.Ten))
                throw new NameDuplicatedException("Tên không được trùng");
            return _phucLoiRepository.Add(phucLoi);
        }

        public void Delete(int id)
        {
            var phucLoi = _phucLoiRepository.GetSingleById(id);
            phucLoi.Status = false;
            _phucLoiRepository.Update(phucLoi);
        }

        public IEnumerable<PhucLoi> GetAll()
        {
            return _phucLoiRepository.GetMulti(x => x.Status == true);
        }

        public IEnumerable<PhucLoi> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _phucLoiRepository.GetMulti(x => x.Status == true & x.Ten.Contains(keyword));
            else
                return GetAll();
        }

        public PhucLoi GetById(int id)
        {
            return _phucLoiRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(PhucLoi phucLoi)
        {
            if (_phucLoiRepository.CheckContains(x => x.Ten == phucLoi.Ten && x.Id != phucLoi.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _phucLoiRepository.Update(phucLoi);
        }
    }
}
