using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.BLL
{
    public abstract class BaseCommandAcceptor : ICommandAcceptor
    {
        protected ICommandAcceptorFactory Factory { get; private set; }
        protected virtual CommandType CommandType { get; }
        public BaseCommandAcceptor()
        {
        }
        public abstract object Value { get; }

        public virtual string ExpressionString => Value.ToString();

        public abstract ICommandAcceptor Accept(Command command);

        public void Initialize(ICommandAcceptorFactory commandAcceptorFactory, ICommandAcceptor previousAcceptor)
        {
            Factory = commandAcceptorFactory;
            OnInitialize(previousAcceptor);
            
        }
        protected virtual void OnInitialize(ICommandAcceptor previousAcceptor)
        {

        }

        public abstract bool CanAccept(Command command);
    }
}
