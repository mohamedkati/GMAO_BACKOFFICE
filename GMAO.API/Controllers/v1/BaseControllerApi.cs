global using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using AutoMapper;
using MediatR;

namespace GMAO.API.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ApiVersion("1.0")]
    public abstract class BaseControllerApi : ControllerBase
    {
        private IMediator _mediator;
        private IMapper _mapper;
        //public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        public IMediator Mediator
        {
            get
            {
                if (_mediator == null)
                    _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
                return _mediator;
            }
            set
            {
                if (HttpContext != null)
                    _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
                else
                    _mediator = value;
            }

        }

        public IMapper Mapper => _mapper ??= HttpContext.RequestServices.GetService<IMapper>();
    }
}
