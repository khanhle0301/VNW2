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
    [RoutePrefix("api/capbac")]
    [Authorize]
    public class CapBacController : ApiControllerBase
    {
        private readonly ICapBacService _capBacService;

        public CapBacController(IErrorService errorService, ICapBacService capBacService)
            : base(errorService)
        {
            _capBacService = capBacService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 10, string keyword = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _capBacService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<CapBac>, IEnumerable<CapBacViewModel>>(query);

                var paginationSet = new PaginationSet<CapBacViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, CapBacViewModel capBacVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newCapBac = new CapBac();
                    newCapBac.UpdateCapBac(capBacVm);
                    _capBacService.Add(newCapBac);
                    _capBacService.Save();

                    var responseData = Mapper.Map<CapBac, CapBacViewModel>(newCapBac);
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
        public HttpResponseMessage Update(HttpRequestMessage request, CapBacViewModel capBacVm)
        {
            if (ModelState.IsValid)
            {
                var dbCapBac = _capBacService.GetById(capBacVm.Id);
                dbCapBac.UpdateCapBac(capBacVm);
                try
                {
                    _capBacService.Update(dbCapBac);
                    _capBacService.Save();

                    var responseData = Mapper.Map<CapBac, CapBacViewModel>(dbCapBac);
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
                var model = _capBacService.GetById(id);

                var responseData = Mapper.Map<CapBac, CapBacViewModel>(model);

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
                    _capBacService.Delete(id);
                    _capBacService.Save();
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
                        _capBacService.Delete(item);
                    }

                    _capBacService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }

                return response;
            });
        }
    }
}