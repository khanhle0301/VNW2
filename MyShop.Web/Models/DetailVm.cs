using MyShop.Common.ViewModels;
using MyShop.Model.Models;
using System.Collections.Generic;

namespace MyShop.Web.Models
{
    public class DetailVm
    {
        public TinTuyenDung TinTuyenDung;

        public IEnumerable<KyNangVm> KyNang;

        public IEnumerable<PhucLoiVm> PhucLoi;

        public IEnumerable<NganhNgheVm> NganhNghe;

        public IEnumerable<TinhVm> Tinh;

        public List<string> ListImages;
    }
}