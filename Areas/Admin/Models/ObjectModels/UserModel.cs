using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ginko_webapp.Areas.Admin.Models.ObjectModels
{
    public class UserModel
    {
        private int id;
        private string name;
        private string password;
        private string token;
        private string email;
        private string phoneNumber;
        private string fullName;
        private string avatar;
        private string bio;
        private string slogan;
        private string job;
        private string birthday;
        private string gender;
        private string address;
        private string role;


        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }
        public string Name 
        {
            get
            {
                return this.name;
            }
            set 
            {
                this.name = (value != null) ?  value : "";

            } 
        }
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = (value != null) ? value : "";

            }
        }
        public string Token
        {
            get
            {
                return this.token;
            }
            set
            {
                this.token = (value != null) ? value : "";

            }
        }
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = (value != null) ? value : "";

            }
        }
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                this.phoneNumber = (value != null) ? value : "";

            }
        }
        public string FullName
        {
            get
            {
                return this.fullName;
            }
            set
            {
                this.fullName = (value != null) ? value : "";

            }
        }
        public string Avatar
        {
            get
            {
                return this.avatar;
            }
            set
            {
                this.avatar = (value != null) ? value : "";

            }
        }
        public string Bio
        {
            get
            {
                return this.bio;
            }
            set
            {
                this.bio = (value != null) ? value : "";
            }
        }
        public string Slogan
        {
            get
            {
                return this.slogan;
            }
            set
            {
                this.slogan = (value != null) ? value : "";

            }
        }
        public string Job
        {
            get
            {
                return this.job;
            }
            set
            {
                this.job = (value != null) ? value : "";

            }
        }
        public string Birthday
        {
            get
            {
                return this.birthday;
            }
            set
            {
                //this.birthday = (value != null) ? value : "";
                if (value == null)
                {
                    this.birthday = "";
                }
                else
                {
                    DateTime convertDate;
                    if (DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out convertDate))
                    {
                        this.birthday = convertDate.ToString();
                    }
                    else
                    {
                        this.birthday = "";
                    }
                }
            }
        }
        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                if (value == null)
                {
                    this.gender = "NaN";
                }
                else if (value == "1")
                {
                    this.gender = "male";
                }
                else if (value == "0")
                {
                    this.gender = "female";
                }

            }
        }
        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = (value != null) ? value : "";

            }
        }
        public string Role
        {
            get
            {
                return this.role;
            }
            set
            {
                this.role = (value != null) ? value : "";

            }
        }
    }
}