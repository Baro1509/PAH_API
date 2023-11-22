﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Request {
    public class UpdateProfileRequest : IValidatableObject{
        [Required(ErrorMessage = "Không được để trống họ tên người dùng")]
        [StringLength(255)]
        public string Name { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        [Required(ErrorMessage = "Không được để trống số điện thoại")]
        [RegularExpression(@"^(\+84|84|0[1-9]|84[1-9]|\+84[1-9])+([0-9]{8})\b$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Không được để trống hình ảnh")]
        [Url]
        public string ProfilePicture { get; set; }

        [Required(ErrorMessage = "Không được để trống giới tính")]
        [Range(0, 1, ErrorMessage = "Giới tính chỉ có thể là nam hoặc nữ")]
        public int Gender { get; set; }

        [Required(ErrorMessage = "Không được để trống ngày tháng năm sinh")]
        public DateTime Dob { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            int age = DateTime.Now.Year - Dob.Year;
            if (DateTime.Now.DayOfYear < Dob.DayOfYear) age = age - 1;
            if (age < 18) {
                yield return new ValidationResult($"Người tham gia cần có độ tuổi từ 18 trở lên", new[] { nameof(Dob) });
            }
        }
    }
}
