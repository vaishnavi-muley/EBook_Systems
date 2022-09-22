//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EBook_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Feedback
    {
        public int FId { get; set; }

        [DisplayName("Enter Name")]
        [Required(ErrorMessage = "Name is Required")]
        public string name { get; set; }

        [DisplayName("Enter Email-Id")]
        [Required(ErrorMessage = "Email-Id is Required")]

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string email { get; set; }


        [DisplayName("Enter Comment")]
        [Required(ErrorMessage = "Comment is Required")]
        public string comment { get; set; }
    }
}
