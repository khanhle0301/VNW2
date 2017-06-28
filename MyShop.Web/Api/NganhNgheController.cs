using AutoMapper;
using MyShop.Common.Exceptions;
using MyShop.Model.Models;
using MyShop.Service;
using MyShop.Web.Infrastructure.Core;
using MyShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyShop.Web.Infrastructure.Extensions;

namespace MyShop.Web.Api
{
    [RoutePrefix("api/nganhnghe")]
    [Authorize]
    public class NganhNgheController : ApiControllerBase
    {
        private readonly INganhNgheService _nganhNgheService;

        public NganhNgheController(IErrorService errorService, INganhNgheService nganhNgheService)
            : base(errorService)
        {
            _nganhNgheService = nganhNgheService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _nganhNgheService.GetAllParent();

                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 10, string keyword = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _nganhNgheService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<NganhNghe>, IEnumerable<NganhNgheViewModel>>(query);

                var paginationSet = new PaginationSet<NganhNgheViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, NganhNgheViewModel nganhNgheVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newNganhNghe = new NganhNghe();
                    newNganhNghe.UpdateNganhNghe(nganhNgheVm);
                    newNganhNghe.CreatedDate = DateTime.Now;
                    newNganhNghe.CreatedBy = User.Identity.Name;
                    newNganhNghe.UpdatedDate = DateTime.Now;
                    newNganhNghe.UpdatedBy = User.Identity.Name;
                    _nganhNgheService.Add(newNganhNghe);
                    _nganhNgheService.Save();

                    var responseData = Mapper.Map<NganhNghe, NganhNgheViewModel>(newNganhNghe);
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
        public HttpResponseMessage Update(HttpRequestMessage request, NganhNgheViewModel nganhNgheVm)
        {
            if (ModelState.IsValid)
            {
                var dbNganhNghe = _nganhNgheService.GetById(nganhNgheVm.Id);
                dbNganhNghe.UpdateNganhNghe(nganhNgheVm);
                dbNganhNghe.UpdatedDate = DateTime.Now;
                dbNganhNghe.UpdatedBy = User.Identity.Name;
                try
                {
                    _nganhNgheService.Update(dbNganhNghe);
                    _nganhNgheService.Save();

                    var responseData = Mapper.Map<NganhNghe, NganhNgheViewModel>(dbNganhNghe);
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
                var model = _nganhNgheService.GetById(id);

                var responseData = Mapper.Map<NganhNghe, NganhNgheViewModel>(model);

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
                    _nganhNgheService.Delete(id);
                    _nganhNgheService.Save();
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
                        _nganhNgheService.Delete(item);
                    }

                    _nganhNgheService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }

                return response;
            });
        }
    }
}