using MyShop.Common.ViewModels;
using System.Collections.Generic;

namespace MyShop.Web.Models
{
    public class TinTuyenDungHomeVm
    {
        public IEnumerable<KyNangVm> ListKyNang;

        public IEnumerable<TinhVm> ListTinh;

        public IEnumerable<NganhNgheVm> ListNganhNghe;

        public IEnumerable<CapBacVm> ListCapBac;

        public string Keyword;

        public string Industry;

        public string Location;
    }
}