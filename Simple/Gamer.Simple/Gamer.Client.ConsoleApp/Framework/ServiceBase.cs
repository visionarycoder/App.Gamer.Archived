using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamer.Client.ConsoleApp.Framework
{

    public abstract class Service<T> : IService<T> where T : class
    {

        protected ILogger<T> logger { get; init; }

        protected Service(ILogger<T> logger)
        {
            this.logger = logger;
        }

    }

    public interface IService<T> where T : class
    {

    }

}