using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BLL
{
    public interface ICommandAcceptorFactory
    {
        ICommandAcceptor Create(Command command, ICommandAcceptor current);
        ICommandAcceptor CreateLiteral();
    }
}
