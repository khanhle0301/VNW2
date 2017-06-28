using MyShop.Common.Exceptions;
using MyShop.Data.Infrastructure;
using MyShop.Data.Repositories;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Service
{
    public interface INganhNgheService
    {
        NganhNghe Add(NganhNghe nganhNghe);

        void Update(NganhNghe nganhNghe);

        void Delete(int id);

        IEnumerable<NganhNghe> GetAll();

        IEnumerable<NganhNghe> GetAll(string keyword);

        IEnumerable<NganhNghe> GetAllParent();

        NganhNghe GetById(int id);

        void Save();

        IEnumerable<NganhNghe> GetAllByChild();
    }

    public class NganhNgheService : INganhNgheService
    {
        private INganhNgheRepository _nganhNgheRepository;
        private IUnitOfWork _unitOfWork;

        public NganhNgheService(INganhNgheRepository nganhNgheRepository, IUnitOfWork unitOfWork)
        {
            _nganhNgheRepository = nganhNgheRepository;
            _unitOfWork = unitOfWork;
        }

        public NganhNghe Add(NganhNghe nganhNghe)
        {
            if (_nganhNgheRepository.CheckContains(x => x.Ten == nganhNghe.Ten))
                throw new NameDuplicatedException("Tên không được trùng");
            return _nganhNgheRepository.Add(nganhNghe);
        }

        public void Delete(int id)
        {
            var nganhNghe = _nganhNgheRepository.GetSingleById(id);
            nganhNghe.Status = false;
            _nganhNgheRepository.Update(nganhNghe);
        }

        public IEnumerable<NganhNghe> GetAll()
        {
            return _nganhNgheRepository.GetMulti(x => x.Status == true);
        }

        public IEnumerable<NganhNghe> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _nganhNgheRepository.GetMulti(x => x.Status == true & x.Ten.Contains(keyword));
            else
                return GetAll();
        }

        public IEnumerable<NganhNghe> GetAllByChild()
        {
            return _nganhNgheRepository.GetMulti(x => x.Status && x.ParentId != null);
        }

        public IEnumerable<NganhNghe> GetAllParent()
        {
            return _nganhNgheRepository.GetMulti(x => x.Status && x.ParentId == null);
        }

        public NganhNghe GetById(int id)
        {
            return _nganhNgheRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(NganhNghe nganhNghe)
        {
            if (_nganhNgheRepository.CheckContains(x => x.Ten == nganhNghe.Ten && x.Id != nganhNghe.Id))
                throw new NameDuplicatedException("Tên không được trùng");
            _nganhNgheRepository.Update(nganhNghe);
        }
    }
}