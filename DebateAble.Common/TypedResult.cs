using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebateAble.Common
{
    public enum TypedResultSummaryEnum
    {
        Unknown = 0,
        ItemNotFound = 1,
        Unauthorized = 2,
        InvalidRequest =3,
        GeneralException =4
    }

    public class TypedResult
    {
        private bool _wasSuccessful;
        private string? _message;
        private Exception? _exception;
        private TypedResultSummaryEnum _summary;
        private bool _nonOp;

        public bool WasSuccessful
        {
            get { return _wasSuccessful; }
            set { _wasSuccessful = value; }
        }

        public bool IsNonOp
        {
            get { return _nonOp; }
            set { _nonOp = value; }
        }

        public string Message
        {
            get
            {
                if(_message == null)
                {
                    return _exception?.Message;
                }
                else
                {
                    return _message;
                }
            }
            set
            {
                _message = value;
            }
        }

        public Exception Exception
        {
            get
            {
                if (_exception == null)
                {
                    if (!_wasSuccessful)
                    {
                        return new Exception(_message ?? "Unknown exception");
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return _exception;
                }
            }
            set
            {
                _exception = value;
            }
        }

        public TypedResultSummaryEnum Summary
        {
            get { return _summary; }
            set { _summary = value; }
        }

        public TypedResult()
        {

        }

        public TypedResult(bool wasSuccessful)
        {
            _wasSuccessful = wasSuccessful;
        }

        public TypedResult(TypedResultSummaryEnum summary, string message)
            :this(false)
        {
            Message = message;
            Summary = summary;
        }

        public TypedResult(string message,Exception ex)
            : this(false)
        {
            Exception = ex;
            Message = message;  
        }
    }

    public class TypedResult<TResult> : TypedResult
    {
        public TResult Payload { get; set; }

        public TypedResult(TResult data)
            :base(true)
        {
            Payload = data;
        }

        public TypedResult(TypedResultSummaryEnum summary, string message)
            :base(summary,message)
        {

        }

        public TypedResult(string message, Exception ex)
            :base(message,ex)
        {

        }

        public static TypedResult<TResult> NonOp()
        {
            var result = new TypedResult<TResult>(default(TResult))
            {
                IsNonOp = true,
                WasSuccessful = true
            };
            return result;
        }
    }

    public class JobResult
    {
        public List<string> Messages { get; set; } = new List<string>();
        public Exception Exception { get; set; } = null;
        public int SuccessfulOperations { get; set; }
        public int FailedOperations { get; set; }

        public void AddMessage(string message)
        {
            this.Messages.Add(message);
        }
    }

    public static class TypedResultExtensions
    {
        public static TypedResult<TTarget> AsResult<TSource,TTarget>(this TypedResult<TSource> source)
        {
            return new TypedResult<TTarget>(default(TTarget))
            {
                Exception = source.Exception,
                Message = source.Message,
                Summary = source.Summary,
                WasSuccessful = source.WasSuccessful
            };
        }
    }
}
