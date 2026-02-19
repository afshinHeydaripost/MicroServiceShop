using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public enum Roles
    {
        Admin,
        User
    }
    public enum OrderStatus
    {
        [Display(Name = "پیش‌نویس")]
        Draft = 1,
        [Display(Name = "در انتظار پرداخت")]
        PendingPayment = 2,
        [Display(Name = "پرداخت شده")]
        Paid = 3,
        [Display(Name = "پرداخت ناموفق")]
        PaymentFailed = 4,
        [Display(Name = "در حال پردازش")]
        Processing = 5,
        [Display(Name = "ارسال شده")]
        Shipped = 6,
        [Display(Name = "تحویل داده شده")]
        Delivered,
        [Display(Name = "لغو شده")]
        Cancelled = 7,
        [Display(Name = "مرجوع شده / بازگشت وجه")]
        Refunded = 8,
    }
}
