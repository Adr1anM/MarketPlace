using AutoMapper;
using MarketPlace.Application.Abstractions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Helpers
{
    public class BaseCommandHandlerTest
    {
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<IMapper> _mapperMock;
        protected readonly Mock<ILoggerFactory> _loggerFactoryMock;


        public BaseCommandHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();

        }

    }
}
