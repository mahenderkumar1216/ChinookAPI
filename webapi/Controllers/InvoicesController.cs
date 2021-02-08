using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShapingAPI.Entities;
using ShapingAPI.Infrastructure.Core;
using ShapingAPI.Infrastructure.Data.Repositories;
using ShapingAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShapingAPI.Controllers
{
    [Route("api/[controller]")]
    public class InvoicesController : ControllerBase
    {
        #region Properties
        private readonly IInvoiceRepository _invoiceRepository;
        private const int maxSize = 50;
        #endregion

        #region Constructor
        public InvoicesController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }
        #endregion

        #region Actions
        public ActionResult Get(string props = null, int page = 1, int pageSize = maxSize)
        {
            try
            {
                var _invoices = _invoiceRepository.LoadAll().Skip((page - 1) * pageSize).Take(pageSize);

                var _invoicesVM = Mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceViewModel>>(_invoices);

                JToken _jtoken = TokenService.CreateJToken(_invoicesVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Route("{invoiceId}")]
        public ActionResult Get(int invoiceId, string props = null)
        {
            try
            {
                var _invoice = _invoiceRepository.Load(invoiceId);

                if (_invoice == null)
                {
                    return BadRequest();
                }

                var _invoiceVM = Mapper.Map<Invoice, InvoiceViewModel>(_invoice);

                JToken _jtoken = TokenService.CreateJToken(_invoiceVM, props);

                return Ok(_jtoken);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        #endregion
    }
}
