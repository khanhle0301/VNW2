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
    [RoutePrefix("api/phucloi")]
    [Authorize]
    public class PhucLoiController : ApiControllerBase
    {
        private readonly IPhucLoiService _phucLoiService;

        public PhucLoiController(IErrorService errorService, IPhucLoiService phucLoiService)
            : base(errorService)
        {
            _phucLoiService = phucLoiService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, int pageSize = 10, string keyword = null)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _phucLoiService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.Id).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<PhucLoi>, IEnumerable<PhucLoiViewModel>>(query);

                var paginationSet = new PaginationSet<PhucLoiViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, PhucLoiViewModel phucLoiVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newPhucLoi = new PhucLoi();
                    newPhucLoi.UpdatePhucLoi(phucLoiVm);
                    _phucLoiService.Add(newPhucLoi);
                    _phucLoiService.Save();

                    var responseData = Mapper.Map<PhucLoi, PhucLoiViewModel>(newPhucLoi);
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
        public HttpResponseMessage Update(HttpRequestMessage request, PhucLoiViewModel phucLoiVm)
        {
            if (ModelState.IsValid)
            {
                var dbPhucLoi = _phucLoiService.GetById(phucLoiVm.Id);
                dbPhucLoi.UpdatePhucLoi(phucLoiVm);
                try
                {
                    _phucLoiService.Update(dbPhucLoi);
                    _phucLoiService.Save();

                    var responseData = Mapper.Map<PhucLoi, PhucLoiViewModel>(dbPhucLoi);
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
                var model = _phucLoiService.GetById(id);

                var responseData = Mapper.Map<PhucLoi, PhucLoiViewModel>(model);

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
                    _phucLoiService.Delete(id);
                    _phucLoiService.Save();
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
                        _phucLoiService.Delete(item);
                    }

                    _phucLoiService.Save();

                    response = request.CreateResponse(HttpStatusCode.OK, listProductCategory.Count);
                }

                return response;
            });
        }
    }
}
