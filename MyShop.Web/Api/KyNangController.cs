using AutoMapper;
using MyShop.Common.Exceptions;
using MyShop.Model.Models;
using MyShop.Service;
using MyShop.Web.Infrastructure.Core;
using MyShop.Web.Infrastructure.Extensions;
using MyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyShop.Web.Api
{
    [RoutePrefix("api/kynang")]
    [Authorize]
    public class KyNangController : ApiControllerBase
    {
        private readonly IKyNangService _kyNangService;

        public KyNangController(IErrorService errorService, IKyNangService kyNangService)
            : base(errorService)
        {
            _kyNangService = kyNangService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 10, string keyword = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _kyNangService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<KyNang>, IEnumerable<KyNangViewModel>>(query);

                var paginationSet = new PaginationSet<KyNangViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, KyNangViewModel kyNangVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newKyNang = new KyNang();
                    newKyNang.UpdateKyNang(kyNangVm);
                    _kyNangService.Add(newKyNang);
                    _kyNangService.Save();

                    var responseData = Mapper.Map<KyNang, KyNangViewModel>(newKyNang);
                    return request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, KyNangViewModel kyNangVm)
        {
            if (ModelState.IsValid)
            {
                var dbKyNang = _kyNangService.GetById(kyNangVm.Id);
                dbKyNang.UpdateKyNang(kyNangVm);
                try
                {
                    _kyNangService.Update(dbKyNang);
                    _kyNangService.Save();

                    var responseData = Mapper.Map<KyNang, KyNangViewModel>(dbKyNang);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _kyNangService.GetById(id);

                var responseData = Mapper.Map<KyNang, KyNangViewModel>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _kyNangService.Delete(id);
                    _kyNangService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, id);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listProductCategory = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<List<int>>(listId);
                    foreach (var item in listProductCategory)
                    {
                        _kyNangService.Delete(item);
                    }

                    _kyNangService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }

                return response;
            });
        }
    }
}