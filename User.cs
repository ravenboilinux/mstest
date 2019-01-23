using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSTest
{
    public class User : DomainBase
    {
        #region Private Fields
        private string loginName;
        private string passwordHash;
        private string salt;

        private static User nullValue = new User();
        #endregion

        #region Properties



        public string LoginName
        {
            get
            {
                return loginName;
            }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }
                if (loginName != value)
                {
                    loginName = value;
                    PropertyHasChanged(nameof(LoginName));
                }
            }
        }


        public string PasswordHash
        {
            get
            {
                return passwordHash;
            }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }
                if (passwordHash != value)
                {
                    passwordHash = value;
                    PropertyHasChanged(nameof(PasswordHash));
                }
            }
        }

        public string Salt
        {
            get
            {
                return salt;
            }
            set
            {
                if (value == null)
                {
                    value = String.Empty;
                }
                if (salt != value)
                {
                    salt = value;
                    PropertyHasChanged(nameof(Salt));
                }
            }
        }


     


        #endregion

        #region Constructors
        public User() : base()
        {

        }

        public User(int id, string loginName, string passwordHash, string salt)
        {
            this.id = id;
            this.loginName = loginName;
            this.passwordHash = passwordHash;
            this.salt = salt;
            this.MarkOld();
        }
        #endregion
        #region Methods




        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            User other = obj as User;
            if (other == null)
                return false;
            return this.GetHashCode().Equals(other.GetHashCode());
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return String.Format(CultureInfo.CurrentCulture, "{0}: {1} ({2})", GetType(), this.loginName, this.id);
        }

        public static User NullValue
        {
            get
            {
                return nullValue;
            }
        }

        #endregion
    }
}
