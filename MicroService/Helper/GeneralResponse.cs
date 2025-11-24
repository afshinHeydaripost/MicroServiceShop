using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class GeneralResponse
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public static string _SubmitAccessDenied { get; set; } = "شما به این بخش دسترسی ندارید";
        public static string _SubmitAccessDeniedToDoc { get; set; } = "این سند توسط شما تنظیم نشده است و امکان ویرایش آن برای شما نیست";
        private static string _SubmitError { get; set; } = "خطا در ثبت اطلاعات";
        private static string _SubmitSuccess { get; set; } = "اطلاعات با موفقیت ثبت شد";
        private static string _SubmitSuccessDelete { get; set; } = "اطلاعات با موفقیت حذف شد";
        private static string _SubmitErrorDelete { get; set; } = "خطا در حذف اطلاعات";
        private static string _SubmitNotFond { get; set; } = "اطلاعات مورد نظر یافت نشد";
        public static string _SubmitAccessDeniedCreator { get; set; } = "این سند توسط شما تنظیم نشده است و امکان تغییر وجود ندارد";

        public static GeneralResponse Success()
        {
            return new GeneralResponse()
            {
                Message = _SubmitSuccess,
                isSuccess = true
            };
        }
        public static GeneralResponse SuccessDelete()
        {
            return new GeneralResponse()
            {
                Message = _SubmitSuccessDelete,
                isSuccess = true
            };
        }
        public static GeneralResponse Success(string message)
        {
            return new GeneralResponse()
            {
                isSuccess = true,
                Message = message
            };
        }

        public static GeneralResponse SuccessMessage()
        {
            return new GeneralResponse()
            {
                isSuccess = true,
                Message = _SubmitSuccess
            };
        }
        public static GeneralResponse SuccessMessageDelete()
        {
            return new GeneralResponse()
            {
                isSuccess = true,
                Message = _SubmitSuccessDelete
            };
        }
        public static GeneralResponse AccessDeniedCreator()
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = _SubmitAccessDeniedCreator
            };
        }
        public static GeneralResponse Fail()
        {
            return new GeneralResponse()
            {
                isSuccess = false
            };
        }
        public static GeneralResponse Fail(string message)
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = message
            };
        }

        public static GeneralResponse FailDelete(Exception e)
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                ErrorMessage = GetError(e),
                Message = _SubmitErrorDelete
            };
        }
        public static GeneralResponse FailDelete()
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = _SubmitErrorDelete
            };
        }
        public static GeneralResponse FailMessage()
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = _SubmitError
            };
        }
        public static GeneralResponse Fail(string message, string errorMessage)
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = message,
                ErrorMessage = errorMessage
            };
        }

        public static GeneralResponse FailDeleteAtt(string userTitle)
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = $"این ضمیمه توسط {userTitle} ثبت شده است و امکان حذف برای شما نیست"
            };
        }

        public static GeneralResponse Fail(Exception e)
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = _SubmitError,
                ErrorMessage = GetError(e)
            };
        }

        public static GeneralResponse NotFound()
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = _SubmitNotFond,
            };
        }
        public static GeneralResponse AccessDenied()
        {
            return new GeneralResponse()
            {
                isSuccess = false,
                Message = _SubmitAccessDenied,
            };
        }

        public static GeneralResponse Response(bool isSuccess, string message, string errorMessage)
        {
            return new GeneralResponse()
            {
                isSuccess = isSuccess,
                Message = message,
                ErrorMessage = errorMessage
            };
        }



        private static string GetError(Exception e)
        {
            if (e.InnerException == null)
                return e.Message;
            return e.InnerException.Message;
        }
    }
    public class GeneralResponse<T>
    {
        public bool isSuccess { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
        public T obj { get; set; }
        public static string _SubmitAccessDenaid { get; set; } = "شما به این بخش دسترسی ندارید";
        private static string _SubmitError { get; set; } = "خطا در ثبت اطلاعات";
        private static string _SubmitSuccess { get; set; } = "اطلاعات با موفقیت ثبت شد";
        private static string _SubmitSuccessDelete { get; set; } = "اطلاعات با موفقیت حذف شد";
        private static string _SubmitErrorDelete { get; set; } = "خطا در حذف اطلاعات";
        private static string _SubmitNotFond { get; set; } = "اطلاعات مورد نظر یافت نشد";
        public static GeneralResponse<T> Success(T objItem, string message)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = true,
                obj = objItem,
                Message = message
            };
        }
        public static GeneralResponse<T> AccessDenied(T objItem)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                obj = objItem,
                Message = _SubmitAccessDenaid,
            };
        }
        public static GeneralResponse<T> Success(T objItem)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = true,
                obj = objItem,
                Message = _SubmitSuccess
            };
        }

        public static GeneralResponse<T> SuccessMessage(T objItem)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = true,
                obj = objItem,
                Message = _SubmitSuccess
            };
        }

        public static GeneralResponse<T> Fail(T objItem)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                obj = objItem,
                Message = _SubmitError
            };
        }
        public static GeneralResponse<T> Fail(T objItem, string message)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                obj = objItem,
                Message = message
            };
        }

        public static GeneralResponse<T> Fail(string message)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                Message = message
            };
        }
        public static GeneralResponse<T> Fail(T objItem, string message, string errorMessage)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                Message = message,
                obj = objItem,
                ErrorMessage = errorMessage
            };
        }

        public static GeneralResponse<T> NotFound(T objItem)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                obj = objItem,
                Message = _SubmitNotFond,
            };
        }

        public static GeneralResponse<T> Fail(T objItem, Exception e)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                Message = _SubmitError,
                obj = objItem,
                ErrorMessage = GetError(e)
            };
        }
        public static GeneralResponse<T> Fail(Exception e)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = false,
                Message = _SubmitError,
                ErrorMessage = GetError(e)
            };
        }

        public static GeneralResponse<T> Response(T objItem, bool isSuccess, string message, string errorMessage)
        {
            return new GeneralResponse<T>()
            {
                isSuccess = isSuccess,
                Message = message,
                ErrorMessage = errorMessage,
                obj = objItem,
            };
        }



        private static string GetError(Exception e)
        {
            if (e.InnerException == null)
                return e.Message;
            return e.InnerException.Message;
        }
    }
}
