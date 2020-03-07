using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WAnoHTTPS.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "خطا: وارد کردن آدرس ایمیل ضروری است")]
        public string emailaddress_viewmodel { get; set; }

        public string password_viewmodel { get; set; }
        [Compare("password_viewmodel", ErrorMessage = "هر دو پسوورد وارد شده باید کاملا یکسان باشند")]
        public string repassword_viewmodel { get; set; }

        [Required(ErrorMessage = "خطا: حداقل باید به یه اسمی صدات کنم")]
        public string firstname_viewmodel { get; set; }
        public string lastname_viewmodel { get; set; }
    }
}
